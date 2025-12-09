//ВО ВСЕХ ПРИМЕРАХ ЧАСТЬ КОДА ЗАМЕНЕНА НА ... для упрощения
/*
Данный хэндлер используется для экспорта данных
При этом, является неким "каркасом", который через generic ограничения позволяет обрабатывать только конкретные команды 
экспорта.
Причём здесь под экспортом понимается только экспорт данных для так называемых "сверок" -- это выгрузка данных для 
сравнения и поиска расхождений в разных системах по определенному ключевому полю за один момент времени.
Т.е. незнакомые с проектом люди только из названия хэндлера точно не поймут, о каком экспорте речь.
С учётом комментария также можно добавить, что на основании этого каркаса построена потэнциально бесконечно 
расширяемая система (на текущий момент уже более 70 хэндлеров наследников)
Команда медиатора для данного хэндлера выбирается на основании задания на экспорт (по типу экспортируемой сущности), 
а результат работы (Success, Error) нужен для установки финального статуса задания на экспорт
*/
internal abstract class BaseExportCommandInternalHandler<TCommand, T>
    : IRequestHandler<TCommand, CommandResult> where TCommand : BaseExportCommandInternal
{
    ...

    protected abstract ExportType ExportType { get; }

    protected BaseExportCommandInternalHandler(...)
    {
    }

    public async Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        CommandResult result;

        var sw = Stopwatch.StartNew();

        var export = new Export(request.ExportId, ExportType);

        try
        {
            bool isExistRequest = await reconciliationRequestsLog.IsExistRequest(cancellationToken);

            if (isExistRequest)
            {
                _logger.LogWarning(GetAlreadyCompletedLogMessage(request.ExportId, request.GetType().Name));

                return CommandResult.CreateSuccess();
            }

            await reconciliationRequestsLog.CreateRequest(cancellationToken);

            await reconciliationRequestsLog.UpdateRequest(ReconciliationRequestStatus.InProgress, cancellationToken);
            string exportStartedMessage = GetExportStartedLogMessage(request.ExportId, request.GetType().Name);

            IAsyncEnumerable<T> dataForRecon = GetDataForRecon(criteria, cancellationToken);
            int counter = await _recordsInsertEventSender.Send(request.TargetTableName, request.ExportId, ExportType, dataForRecon, cancellationToken);
            await _exportEventSender.Exported(export, counter, cancellationToken);
            result = CommandResult.CreateSuccess();
        }
        catch (Exception e)
        {
            await _exportEventSender.ExportError(export, cancellationToken);
            result = CommandResult.CreateError(e);
        }
		...

        return result;
    }