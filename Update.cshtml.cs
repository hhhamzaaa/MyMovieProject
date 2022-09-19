using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;
using static MoviesDatabase.Pages.MoviesModel;

namespace MoviesDatabase.Pages
{
    public class UpdateModel : PageModel
    {
        public MovieInfo movieInfo = new MovieInfo();
        public String ErrorMessage = "";
        public String SuccessMessage = "";

        public void OnGet()
        {
            String MovieId = Request.Query["MovieId"];

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\benab\\OneDrive\\Documents\\MoviesDatabase.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Movies WHERE MovieId = @MovieId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MovieId", movieInfo.MovieId);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                movieInfo.MovieId = "" + reader.GetInt32(0);
                                movieInfo.MovieName = reader.GetString(1);
                                movieInfo.RoomId = reader.GetString(2);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            movieInfo.MovieId = Request.Form["MovieId"];
            movieInfo.MovieRelease = Request.Form["MovieRelease"];
            movieInfo.RoomId = Request.Form["RoomId"];

            if (movieInfo.MovieName.Length == 0 || movieInfo.MovieRelease.Length == 0 || movieInfo.RoomId.Length == 0)
            {
                ErrorMessage = "all fields all required";
                return;
            }

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\benab\\OneDrive\\Documents\\MoviesDatabase.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Movies" +
                        "SET MovieName = @MovieName, MovieRelease = @MovieRelease, RoomId = @RoomId" + 
                        "WHERE MovieId = @MovieId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MovieId", movieInfo.MovieId);
                        command.Parameters.AddWithValue("@MovieName", movieInfo.MovieName);
                        command.Parameters.AddWithValue("@MovieRelease", movieInfo.MovieRelease);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            Response.Redirect("Movies");
        }
    }
}
