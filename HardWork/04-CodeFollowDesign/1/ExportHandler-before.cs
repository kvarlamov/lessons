using Microsoft.Extensions.Logging;

namespace HardWork._04_CodeFollowDesign._1;

internal abstract class BaseExportHandler<TCommand, THandler> : IRequestHandler<TCommand>
        where TCommand : IRequest
        where THandler : IRequestHandler<TCommand>
    {
        private readonly Func<ExportTaskType, IExportStrategy> _exportFactory;
        protected readonly IExportTaskRepository ExportTaskRepository;
        private readonly ILogger<THandler> _logger;

        public BaseExportHandler(
            Func<ExportTaskType, IExportStrategy> exportFactory,
            IExportTaskRepository exportTaskRepository,
            ILogger<THandler> logger)
        {
            _exportFactory = exportFactory;
            ExportTaskRepository = exportTaskRepository;
            _logger = logger;
        }

        public async Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            ExportTask? task = await GetExportTask(request, cancellationToken);

            if (task is null)
            {
                return;
            }

            try
            {
                _logger.LogInformation("Запущена задача экспорта  {taskType} за {date}", task.ExportTaskType.ToString(), task.Date.ToShortDateString());
                IExportStrategy exportStrategy = _exportFactory(task.ExportTaskType);
                int exportedCount = await exportStrategy.Process(task, cancellationToken);

                task.SetExportedRowCount(exportedCount);
                task.SetCompleted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка экспорта {taskType} за {date}", task.ExportTaskType.ToString(), task.Date.ToShortDateString());
                task.SetError(e.Message);
            }
            finally
            {
                await ExportTaskRepository.Update(task, CancellationToken.None);
                _logger.LogInformation("Закончен экспорт {taskType} за {date}", task.ExportTaskType.ToString(), task.Date.ToShortDateString());
            }
        }

        protected abstract Task<ExportTask?> GetExportTask(TCommand request, CancellationToken cancellationToken);
    }

internal interface IRequest
{
}

internal interface IRequestHandler<T>
{
}

public class ExportTask
{
    public void SetExportedRowCount(int exportedCount)
    {
        throw new NotImplementedException();
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

public interface IExportTaskRepository
{
    Task Update(ExportTask task, CancellationToken none);
    Task Update(ExportTask2 task, CancellationToken none);
    Task<ExportTask2> Get(ExportTaskType taskType, CancellationToken cancellationToken);
}

public enum ExportTaskType
{
    None = 0,
}

public interface IExportStrategy
{
    Task<int> Process(ExportTask task, CancellationToken cancellationToken);
    Task<ExportTask2> Process(ExportTask2 task, CancellationToken cancellationToken);
}

