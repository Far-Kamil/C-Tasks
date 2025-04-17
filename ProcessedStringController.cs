using Microsoft.AspNetCore.Mvc;
namespace _7_task.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessedStringController : ControllerBase
{
    private readonly IProcessedString processedStringService;
    public ProcessedStringController(IProcessedString processedStringService)
    {
        this.processedStringService = processedStringService;
    }
    [HttpGet]
    public async Task<ActionResult<ProcessedStringData>> GetProcessedStringData([FromQuery] string originalString, [FromQuery] string? sortMethod = "quickSort")
    {
        var ProcStringData = new ProcessedStringData(originalString);
        try
        {
            await processedStringService.ProcessString(ProcStringData, sortMethod);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        return ProcStringData;
    }
}
public class ProcessedStringData
{
    public ProcessedStringData(string originalString)
    {
        OriginalString = originalString;
    }
    public string OriginalString { get; set; }
    public string ProcessedString { get; set; }
    public Dictionary<char, int> CharacterOccurrences { get; set; }
    public string LongestSubstring { get; set; }
    public string SortedString { get; set; }
    public string TruncatedString { get; set; }
}
