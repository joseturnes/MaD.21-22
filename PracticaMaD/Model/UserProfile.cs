
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Es.Udc.DotNet.PracticaMaD.Model
{

using System;
    using System.Collections.Generic;
    using System.Text;

    public partial class UserProfile
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public UserProfile()
    {

        this.Comment = new HashSet<Comment>();

        this.ImageUpload = new HashSet<ImageUpload>();

        this.UserProfile1 = new HashSet<UserProfile>();

        this.UserProfile2 = new HashSet<UserProfile>();

        this.ImageUpload1 = new HashSet<ImageUpload>();

    }


    public long usrId { get; set; }

    public string loginName { get; set; }

    public string enPassword { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string email { get; set; }

    public string language { get; set; }

    public string country { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Comment> Comment { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ImageUpload> ImageUpload { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<UserProfile> UserProfile1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<UserProfile> UserProfile2 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ImageUpload> ImageUpload1 { get; set; }

        /// <summary>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures 
        /// like a hash table. It uses the Josh Bloch implementation from "Effective Java"
        /// Primary key of entity is not included in the hash calculation to avoid errors
        /// with Entity Framework creation of key values.
        /// </summary>
        /// <returns>
        /// Returns a hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + (loginName == null ? 0 : loginName.GetHashCode());
                hash = hash * multiplier + (enPassword == null ? 0 : enPassword.GetHashCode());
                hash = hash * multiplier + (firstName == null ? 0 : firstName.GetHashCode());
                hash = hash * multiplier + (lastName == null ? 0 : lastName.GetHashCode());
                hash = hash * multiplier + (email == null ? 0 : email.GetHashCode());
                hash = hash * multiplier + (language == null ? 0 : language.GetHashCode());
                hash = hash * multiplier + (country == null ? 0 : country.GetHashCode());

                return hash;
            }

        }

        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
        /// <remarks>See http://www.loganfranken.com/blog/687/overriding-equals-in-c-part-1/ for detailed info </remarks>
        public override bool Equals(object obj)
        {

            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type? 

            UserProfile target = obj as UserProfile;

            return true
               && (this.usrId == target.usrId)
               && (this.loginName == target.loginName)
               && (this.enPassword == target.enPassword)
               && (this.firstName == target.firstName)
               && (this.lastName == target.lastName)
               && (this.email == target.email)
               && (this.language == target.language)
               && (this.country == target.country)
               ;

        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
    	public override String ToString()
        {
            StringBuilder strUserProfile = new StringBuilder();

            strUserProfile.Append("[ ");
            strUserProfile.Append(" usrId = " + usrId + " | ");
            strUserProfile.Append(" loginName = " + loginName + " | ");
            strUserProfile.Append(" enPassword = " + enPassword + " | ");
            strUserProfile.Append(" firstName = " + firstName + " | ");
            strUserProfile.Append(" lastName = " + lastName + " | ");
            strUserProfile.Append(" email = " + email + " | ");
            strUserProfile.Append(" language = " + language + " | ");
            strUserProfile.Append(" country = " + country + " | ");
            strUserProfile.Append("] ");

            return strUserProfile.ToString();
        }

    }

}
