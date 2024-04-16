namespace ChatAppGUI.MVVM.Model
{
    public class ContactModel
    {
        public string Nickname { get; set; }
        public int Id { get; set; }
        public string Information { get; set; }
        
        public ContactModel(string nickname, int id, string message)
        {
            Nickname = nickname;
            Id = id;
            Information = message;
        }

        public ContactModel() { }
    }
}