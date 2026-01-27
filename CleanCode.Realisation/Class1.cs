namespace CleanCode.Realisation;

/*
 * 1. МЕТОДЫ, КОТОРЫЕ РАБОТАЮТ ТОЛЬКО В ТЕСТАХ
 * Нашёл много методов, которые являются фабриками -- генерируют необходимые объекты
 * (в случаях, когда моки не подходят)
 * Пример (названия изменены на упрощённые)
 */
public class Example1_Before
{
    public static object GenerateSomeObject()
    {
        return new User
        {
            Id = "1",
            Name = "John Doe",
        };
    }

    private class User
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
    }
}

/*
 * Заменил на статические (фабричные) методы в самом классе
 * которые Будут использоваться как в тестах, так и в логике приложения
 */
public class Example1_After
{
    public class User
    {
        public string Id { get; }
        public string Name { get; }

        private User(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public static User Create(string id, string name)
        {
            return new User(id, name);
        }
    }
}

// ------------------------------------------------------------------------------------------------------------
/*
 * 2. Цепочки методов
 */

/*
    ДО рефакторинга
    Метод SetupReconciliationPlatformClientWithReconResults используется в тестах для создания 
    моков вызова внешнего клиента, при этом каждый из объектов-наследников ReconRealDiscrepancyBase
    определяется через pattern matching в соответствующем методе в цепочке
    -- цепочка скрывает что реально происходит в методе, кажется, что "сетапится" всё
*/
  
internal void SetupReconciliationPlatformClientWithReconResults(
    ReconRealDiscrepancyBase reconRealDiscrepancy,
    string tableName,
    ReconciliationStatus status)
{
    _externalClient
        .SetupDefault()
        .SetupWithReconResults(reconRealDiscrepancy, tableName, status)
        .SetupCX1Calculation(reconRealDiscrepancy)
        .SetupCX2Calculation(reconRealDiscrepancy)
        .SetupCX3Calculation(reconRealDiscrepancy)
        .SetupCX4Calculation(reconRealDiscrepancy)
        .SetupCX5Calculation(reconRealDiscrepancy);
}

// Пример как выглядит конфигурирование:
internal static Mock<IGoReconciliationPlatformClient> SetupCX1Calculation(
    this Mock<IGoReconciliationPlatformClient> mock,
    ReconRealDiscrepancyBase reconRealDiscrepancy)
{
    if (reconRealDiscrepancy is CX1ReconRealDiscrepancy cx1ReconDiscrepancy)
    {
        mock.Setup(platform => platform.GetCX1RealDiscrepancy(
                It.IsAny<ReconciliationRunId>(),
                It.IsAny<ReconciliationRunId>(),
                It.IsAny<ReconciliationTable>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => cx1ReconDiscrepancy);
    }

    return mock;
}

/*
 * Рефакторинг можно провести следующим образом:
 * Создать отдельный метод, который в зависимости от типа будет выполнять мок
 * Т.о. вместо цепочки вызовов -- явно определяем, какой метод выполняется
 * Правда появляется другая проблема -- ипсользование if.
 * Её нужно решать отдельно, например через полиморфизм, но в рамках данного задания ограничиваюсь if (switch-case)
 * + для тестирования читаемость кажется более предпочтительной
 */

internal void SetupReconciliationPlatformClientWithReconResults(
    ReconRealDiscrepancyBase reconRealDiscrepancy,
    string tableName,
    ReconciliationStatus status)
{
    //...
    Setup(reconRealDiscrepancy);

    void Setup(ReconRealDiscrepancyBase rec)
    {
        switch (rec)
        {
            case GetCX1RealDiscrepancy cx1:
                SetupCx1(cx1);
                break;
      
            case GetCX2RealDiscrepancy cx2:
                SetupCx2(cx2);
                break;
        
            //... другие типы
            
            default:
                break;
        }
    }
    //...
}

// ------------------------------------------------------------------------------------------------------------
// 3. Слишком большой список параметров:
// before:
Task<IReadOnlyCollection<CX1AsgardStockTypeDiscrepancy>> GetCX1AsgardStockTypeDiscrepancy(
    ReconciliationRunId lastLaunchRunId,
    ReconciliationRunId previousLaunchRunId,
    ReconciliationTable tableName,
    ReconCalculationType calculationType,
    CancellationToken cancellationToken);
    
    
// after:    
// выделил отдельный класс dto с информацией (при добавлении информации сможем расширять класс без изменения метода)
// количество параметров сократилось до 3-х с 5-ти -- calculationType не стал класть в дто поскольку он принадлежит
// другой сущности
// 
public sealed record ReconciliationInfo(
    ReconciliationRunId LastLaunchRunId,
    ReconciliationRunId PreviousLaunchRunId,
    ReconciliationTable TableName);
    
    
Task<IReadOnlyCollection<CX1AsgardStockTypeDiscrepancy>> GetCX1AsgardStockTypeDiscrepancy(
    ReconciliationInfo info,
    ReconCalculationType calculationType,
    CancellationToken cancellationToken);
    
    
// ------------------------------------------------------------------------------------------------------------
// 4. Странные решения
/*
 * Не смог найти в своём рабочем коде кода по данному примеру,
 * но нашёл код коллеги -- пример -- Интерфейс работы с репозиторием
 */
 
// Пример 1 -- два метода получения некоторого множества Movement
// 1-й - Получение асинхронного стрима по коллекции 
IAsyncEnumerable<Movement> GetAsAsyncStream(
    IReadOnlyCollection<MovementModel> movements,
    CancellationToken cancellationToken);

// 2-й Получение коллекции по коллекции движений
Task<IReadOnlyCollection<Movement>> GetAsCollection(
    IReadOnlyCollection<MovementModel> movements,
    CancellationToken cancellationToken);
    
// Второй метод при этом является избыточным, поскольку в случаях, где нам не нужен асинхронный стрим
// мы всегда можем получить список через linq метод ToListAsync
// При этом наличие обоих методов вводит в заблуждение, приходится смотреть реализацию обоих методов

    
// Пример 2 - в том же интерфейсе
// Метод получения коллекции движений
Task<IReadOnlyCollection<Movement>> Search(
    string client,
    string operationId,
    CancellationToken cancellationToken);

// Метод получения стрима по коллекции    
IAsyncEnumerable<Movement> GetByClientOperationIds(
    IReadOnlyCollection<ClientOperationId> clops,
    CancellationToken cancellationToken);
    
// Фактически второй метод GetByClientOperationIds всегда можно использовать вместо метода Search,
// передавая в коллекции 1-н аргумент (в 1-м методе также неверный параметр, поскольку есть подходящая структура
// ClientOperationId

// ------------------------------------------------------------------------------------------------------------
// 1.5. Чрезмерный результат
/*
 * По данному примеру не нашлось найти подходящего кода
 * При этом немного порефлексировал, и в целом кажется (возможно ошибочно), что он может вступать в противоречие с п4
 * - странные решения, поскольку избавляясь от лишних методов мы можем сделать метод более универсальным,
 * при этом в некоторых местах использования результат работы метода будет использован не полностью
 */