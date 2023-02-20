namespace MarketDataGateway.Model;

/// <summary>
/// Simple exception to signal a similar quote already exist in the store.
/// I keep it empty but ideally it would include some more details such as the quote, maybe the Id of the
/// duplicate contribution, etc...
/// </summary>
public class QuoteAlreadyExistException : ApplicationException
{
    
}