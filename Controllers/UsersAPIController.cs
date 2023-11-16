using Microsoft.AspNetCore.Mvc;
using TugasRPLBKMod9_Kel29.Models.DTO;
using TugasRPLBKMod9_Kel29.Data;
using Microsoft.AspNetCore.Identity;

namespace TugasRPLBKMod9_Kel29.Controllers
{
    [Route("api/UsersAPI")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UsersDTO>> GetUser()
        {
            return Ok(UsersStore.userList);
        }
        [HttpGet("{Id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UsersDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]

        public ActionResult<UsersDTO> GetUser(int  id)
        {
            if (id == 0) return BadRequest(string.Empty);  
            var acc = UsersStore.userList.FirstOrDefault(x => x.Id == id);
            if (acc == null) return NotFound();
            return Ok(acc);
        }
        [HttpPost]
        public ActionResult<UsersDTO> CreateAcc([FromBody] UsersDTO user)
        {
            if (UsersStore.userList.FirstOrDefault(x => x.Username.ToLower() == user.Username.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Akun sudah ada yang punya");
            }
            if (user == null) return BadRequest(user);
            user.Id = UsersStore.userList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            UsersStore.userList.Add(user);
            string response = "Sukses menambahkan data Akun" + "\nNama : " + user.Username;
            return CreatedAtRoute("GetUser", new { id = user.Id }, response);

        }
        [HttpPost("/login")]
        public ActionResult<UsersDTO> LoginAcc([FromBody] UsersDTO user)
        {
            if (user == null)
            {
                return BadRequest("Username/Password Salah");
            }

            var User = UsersStore.userList.FirstOrDefault(u => u.Username == user.Username);
            if (user == null)
            {
                return NotFound("Username gaada");
            }

            if (user.Password != user.Password)
            {
                return Unauthorized("Password Salah");
            }
            return Ok("Login Berhasil");
        }
        [HttpDelete("{id:int}", Name = "DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var acc = UsersStore.userList.FirstOrDefault(x => x.Id == id);
            if (acc == null)
            {
                return NotFound();
            }
            UsersStore.userList.Remove(acc);
            return Ok("Akun Berhasil Dihapus");
        }
        [HttpPut("{id:int}", Name = "UpdateUser")]
        public IActionResult UpdateUser(int id, [FromBody] UsersDTO user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest("Gagal diedit");
            }
            var acc = UsersStore.userList.FirstOrDefault(u => u.Id == id);
            acc.Username = user.Username;
            acc.Password = user.Password;
            return Ok("Berhasil diedit");
        }
    }
}
