using System.Diagnostics.Contracts;
using Microsoft.Extensions.Logging;

namespace HardWork._04_CodeFollowDesign._1;

internal abstract class BaseExportHandlerRef<TCommand, THandler> : IRequestHandler<TCommand>
        where TCommand : IRequest
        where THandler : IRequestHandler<TCommand>
    {
        private readonly IExportProcessor _exportProcessor;
        
        private readonly ILogger<THandler> _logger;

        public BaseExportHandlerRef(
            IExportProcessor exportProcessor,
            ILogger<THandler> logger)
        {
            _exportProcessor = exportProcessor;
            _logger = logger;
        }

        public async Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _exportProcessor.Process(ExportTaskType.None, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }

internal interface IExportProcessor
{
    /// <summary>
    /// Выполняет получение и обработку экспорта по типу
    /// </summary>
    /// <param name="taskType">Precondition: Not None</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Process(ExportTaskType taskType, CancellationToken cancellationToken);
}

public class ExportProcessor : IExportProcessor
{
    private readonly IExportTaskRepository _exportTaskRepository;
    private readonly Func<ExportTask2, IExportStrategy> _exportFactory;
    private readonly ILogger<ExportProcessor> _logger;
    
    public ExportProcessor(IExportTaskRepository exportTaskRepository, ILogger<ExportProcessor> logger)
    {
        _exportTaskRepository = exportTaskRepository;
        _logger = logger;
    }
    
    public async Task Process(ExportTaskType taskType, CancellationToken cancellationToken)
    {
        // предусловие
        Contract.Requires(taskType != ExportTaskType.None);
        
        ExportTask2 task = await _exportTaskRepository.Get(taskType, cancellationToken);
        
        // Если задача пустая -- будет запущена NullExportStrategy -- которая уже определит необходимые действия --
        // логгирование или исключениt
        IExportStrategy exportStrategy = _exportFactory(task);
        ExportTask2 processedTask = await exportStrategy.Process(task, cancellationToken);
        
        //Постусловия (IExportStrategy)
        Contract.Ensures(processedTask != null);
        Contract.Ensures(processedTask!.Error is null);
        await _exportTaskRepository.Update(task, CancellationToken.None);
    }
}

public class NullExportTask : ExportTask2
{
    public NullExportTask(bool isNull) : base(isNull)
    {
    }
}

public class ExportTask2
{
    public string? Error { get; private set; }
    public void SetExportedRowCount(int exportedCount)
    {
        throw new NotImplementedException();
    }
    
    public bool IsNull { get; }

    protected ExportTask2(bool isNull)
    {
        IsNull = isNull;
    }

    public static ExportTask2 GetExportTask(ExportTaskType taskType)
    {
        return new ExportTask2(isNull:false);
    }

    public static NullExportTask GetEmptyTask()
    {
        return new NullExportTask(isNull:true);
    }

    public ExportTaskType ExportTaskType { get; set; }
    public DateTime Date { get; set; }

    public void SetCompleted()
    {
        throw new NotImplementedException();
    }

    public void SetError(string eMessage)
    {
        throw new NotImplementedException();
    }
}
