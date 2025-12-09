//ВО ВСЕХ ПРИМЕРАХ ЧАСТЬ КОДА ЗАМЕНЕНА НА ... для упрощения
/*
Из названия класса видно, что речь о каком-то энричере -- обогатителе. Но без комментариев понять это достаточно 
сложно без анализа кода класса и проекта.
Фактически данный класс обогащает Задачу С Операцией движения товара (далее задача) -- сущность, в которой хранится как 
само движение товара (через композицию общего интерфейса), так и метаданные о проведении движения через 
систему - название топика, статус задачи, ошибка, количество операций перезапуска ошибочных операций, 
следующее время перезапуска...

И несмотря на то, что он называется MovementOperationTask enricher, фактически используется для обогащения
Движения - MovementOperation
Более того происходит обогащение только Даты, другие поля не затрагиваются. И при расширении логики корректнее 
добавлять новые энричеры, имплементирующие интерфейс.
Вызов обогащения происходит в момент проведения движения (при поступлении из топика) -- после обогащения движение 
при отсутствии ошибок отправляется дальше в выходной топик серсиса.
*/

internal sealed class MovementOperationTaskEnricher : IMovementOperationTaskEnricher
{
    private readonly IBatchingFacadeOperationsClient _batchingFacadeOperationsClient;

    public MovementOperationTaskEnricher(IBatchingFacadeOperationsClient batchingFacadeOperationsClient)
    {
        _batchingFacadeOperationsClient = batchingFacadeOperationsClient;
    }

    public async Task Enrich(IReadOnlyCollection<MovementOperationTask> movementOperationTasks, CancellationToken cancellationToken)
    {
        MovementOperationTask[] inventoryOperations = movementOperationTasks
            .Where(z => z.SourceType == MovementOperationSourceType.Inventory)
            .ToArray();

        if (inventoryOperations.Length == 0)
        {
            return;
        }

        BatchingFacadeResult batchingFacadeResult =
            await _batchingFacadeOperationsClient.Get(
                inventoryOperations
                    .Select(z => z.OperationId)
                    .ToHashSet(),
                cancellationToken);

        string errorMessage = batchingFacadeResult.Error is null
            ? "Operation not found in batching facade"
            : batchingFacadeResult.Error.ToString();

        foreach (MovementOperationTask task in inventoryOperations)
        {
            if (batchingFacadeResult.Error is not null || !batchingFacadeResult.Operations.ContainsKey(task.OperationId))
            {
                task.Fail(MovementOperationTaskError.ProcessingDateFailed(errorMessage));

                continue;
            }

            try
            {
                BatchingFacadeOperation operation = batchingFacadeResult.Operations[task.OperationId];
                task.Data.EnrichOperation(MovementOperationEnrichedData.UpdateProcessingDate(operation.CreatedAt));
            }
            catch (Exception e)
            {
                task.Fail(MovementOperationTaskError.ProcessingDateFailed(e.ToString()));
            }
        }
    }
}