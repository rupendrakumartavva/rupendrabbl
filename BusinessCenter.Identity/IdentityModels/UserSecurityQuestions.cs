namespace BusinessCenter.Identity.IdentityModels
{
    public class UserSecurityQuestions 
   {
        public int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual  int SqId { get; set; }
        public virtual string UserAnswer { get; set; }
    }
}
