namespace IconicFund.Services.IServices
{
    public interface IHasherService
    {
        string ComputeSha256Hash(string data);

        string Convert256BytesToString(byte[] bytes);
    }
}
