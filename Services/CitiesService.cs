using ServiceContracts;
using ServiceContracts1;
namespace Services
{

    public class CitiesService
    {
        private List<String> _cities;

        public CitiesService()
        {
            _cities = new List<String>()
            {
                "London", "Paris", "New York"
            };
        }
        public List<string> GetCities() 
        {
            return _cities;
        }
    }
}