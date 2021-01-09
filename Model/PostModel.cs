using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GrapecityAssignment.Model
{
    [DataContract, Serializable]
    public class PostModel
    {
        
        public PostModel()
        {
            Comments = new HashSet<CommentsModel>();
        }
        private int _id;
        [DataMember]
        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _userid;
        [DataMember]
        [Required]
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        private string _title;
        [DataMember]
        [Required]
        [StringLength(50)]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _content;
        [DataMember]
        [Required]
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private DateTime _createdDate;
        [DataMember]
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        private DateTime _modifiedDate;
        [DataMember]
        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        private CrudOperationType _crudOperationType;
        [DataMember]
        [NotMapped]
        public CrudOperationType CrudOperationType
        {
            get { return _crudOperationType; }
            set { _crudOperationType = value; }
        }
        public virtual ICollection<CommentsModel> Comments { get; set; }


        [DataMember]
        [ForeignKey("UserId")]
        public virtual UserDetailsModel UserDetails { get; set; }
    }

    public enum CrudOperationType
    {
        Create,
        Update,
        Delete,
        Read
    }
}
