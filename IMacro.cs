namespace Macro;

public interface IMacro {
    IEnumerable<IEnumerable<string>> GetAllDuplicates(string directory, float threshold);
    IEnumerable<string> GetDuplicatesOf(string filepath, float threshold);
    IEnumerable<string> GetDuplicatesOf(string filepath, string directory, float threshold);
}
