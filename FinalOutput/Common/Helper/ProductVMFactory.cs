
using FinalOutput.Services;
using FinalOutput.ViewModels;

namespace FinalOutput.Common.Helper
{
    /// <summary>
    /// Class that handles creation of ProductViewModel instance.
    /// </summary>
    public static class ProductVMFactory
    {
        /// <summary>
        /// Creates ProductVM instance with configuration.
        /// </summary>
        /// <param name="folderPath">The folder path for read/write operations.</param>
        /// <returns>A ProductVM instance.</returns>
        public static ProductVM CreateViewModel(string folderPath)
        {
            var _productService = new ProductService(folderPath);
            var _productVM = new ProductVM(_productService);
            
            return _productVM;
        }
    }
}
