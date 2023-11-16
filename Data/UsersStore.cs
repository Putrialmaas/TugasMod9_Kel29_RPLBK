using TugasRPLBKMod9_Kel29.Models.DTO;

namespace TugasRPLBKMod9_Kel29.Data
{
    public class UsersStore
    {
        public static List<UsersDTO> userList = new List<UsersDTO>
        {
             new UsersDTO{Id=1, Username="Rifky", Password="Rifky123"},
             new UsersDTO{Id=2, Username="Putri", Password="Putri123"},
             new UsersDTO{Id=3, Username="Galih", Password="Galih123"},
             new UsersDTO{Id=4, Username="Danil", Password="Danil123"}
        };
    }
}
