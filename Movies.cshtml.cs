using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MoviesDatabase.Pages
{
    public class MoviesModel : PageModel
    {
        public List<MovieInfo> listMovies = new List<MovieInfo>();

        public class MovieInfo
        {
            public String MovieId = "";
            public String MovieName = "";
            public String MovieRelease = "";
            public String RoomId = "";
        }

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\benab\\OneDrive\\Documents\\MoviesDatabase.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Movies";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MovieInfo movieInfo = new MovieInfo();
                                movieInfo.MovieId = "" + reader.GetInt32(0);
                                movieInfo.MovieName = reader.GetString(1);
                                movieInfo.MovieRelease = reader.GetString(2);
                                movieInfo.RoomId = reader.GetString(3);

                                listMovies.Add(movieInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception" + ex.ToString());
            }
        }
    }


}
