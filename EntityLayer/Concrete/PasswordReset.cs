using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class PasswordReset
    {
        [Key]
        public int Id { get; private set; }

        [DataType(DataType.Date)]
        public DateTime UsedAt { get; private set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; private set; }

        [DataType(DataType.Date)]
        public DateTime ValidUntil { get; private set; }

        [DataType(DataType.Text)]
        public Guid Token { get; private set; }

        public int UserId { get; private set; }

        public User User { get; private set; }

        public void MarkUsed(string input_token) {
            
            if (input_token.Equals(this.Token))
            {
                if (this.ValidUntil.CompareTo(DateTime.Now) < 0)
                {
                    if (this.CreatedAt.CompareTo(this.UsedAt) < 0) this.UsedAt = DateTime.Now;
                }
                else {
                    throw new Exception("Reset token is invalid.");
                }
                
            }
            else {
                throw new Exception("Reset token is incorrect.");
            }
            
        }

        static public PasswordReset NewReset(User u) {
            PasswordReset _res = new PasswordReset();
            _res.User = u;
            _res.UserId = u.Id;
            _res.CreatedAt = DateTime.Now;
            _res.ValidUntil = DateTime.Now.AddDays(14);
            _res.Token = Guid.NewGuid();
            _res.UsedAt = DateTime.Parse("01.01.1970 00:01");
            return _res;
        }

    }
}
