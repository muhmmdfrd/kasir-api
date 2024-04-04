using Microsoft.Extensions.Options;

namespace KasirApi.Api.Commons
{
    public class StaticFileDetector
    {
        private StaticFileOptions _options;

        public StaticFileDetector(IOptions<MyStaticFileOptions> options)
        {
            _options = options.Value;
        }

        public bool FileExists(PathString path)
        {
            var baseUrl = _options.RequestPath;

            if (!path.StartsWithSegments(baseUrl, out PathString relativePath))
            {
                return false;
            }

            var provider = _options.FileProvider;
            
            if (provider == null)
            {
                return false;
            }

            var file = provider.GetFileInfo(relativePath.Value);

            return !file.IsDirectory && file.Exists;
        }
    }

    public class MyStaticFileOptions : StaticFileOptions
    {
    }
}
