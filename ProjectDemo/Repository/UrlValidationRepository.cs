namespace ProjectDemo.Repository
{
    public class UrlValidationRepository : IUrlValidationRepository
    {
        public bool ValidateUrl(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return false;
                }

                // Check if the URL is well-formed
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri resultUri)
                    && (resultUri.Scheme == Uri.UriSchemeHttp || resultUri.Scheme == Uri.UriSchemeHttps))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while validating URL in UrlValidationRepository.cs: {ex.Message}");
            }
        }
    }
}
