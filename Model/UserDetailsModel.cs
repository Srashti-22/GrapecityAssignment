using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GrapecityAssignment.Model
{
    [DataContract, Serializable]
    public class UserDetailsModel
    {
        public UserDetailsModel()
        {
            Post = new HashSet<PostModel>();
        }
        private int _id;
        [DataMember]
        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _userName;
        [DataMember]
        [Required]
        [StringLength(200)]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _userPassword;
        [DataMember]
        [Required]
        [PasswordPropertyText]
        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }
        private string _userEmail;
        [DataMember]
        [Required]
        [EmailAddress]
        public string UserEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        private string _userContact;
        [DataMember]
        [Required]
        public string UserContact
        {
            get { return _userContact; }
            set { _userContact = value; }
        }


        public virtual ICollection<PostModel> Post { get; set; }

    }
}
