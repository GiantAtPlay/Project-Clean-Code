namespace Application.Domain.Files
{
    public interface IFileService
    {
        void SaveFile(string directory, string name);
    }
}