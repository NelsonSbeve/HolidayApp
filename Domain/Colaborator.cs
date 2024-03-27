namespace Domain;


using System.Net.Mail;

public class Colaborator : IColaborator
{	
	public int Id { get; set; } 
    public string _strName {get; set;}
    public string _strEmail {get; set;}

    public Colaborator(string strName, string strEmail) {

		if( isValidParameters(strName, strEmail) ) {
			_strName = strName;
			_strEmail = strEmail;
		}
		else
			throw new ArgumentException();
	}


	
	public bool isValidParameters(string strName, string strEmail) {

		if( strName==null ||
			strName.Length > 50 ||
			string.IsNullOrEmpty(strName) ||
			ContainsAny(strName, ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]))
			return false;

		if( !IsValidEmailAddres( strEmail ) )
			return false;
		
		return true;
	}

	private bool ContainsAny(string stringToCheck, params string[] parameters)
	{
		return parameters.Any(parameter => stringToCheck.Contains(parameter));
	}

	// from https://mailtrap.io/blog/validate-email-address-c/
	public bool IsValidEmailAddres(string email)
	{
		var valid = true;

		try
		{
			var emailAddress = new MailAddress(email);
		}
		catch
		{
			valid = false;
		}

		return valid;
	}

	public string GetName(){
		return _strName;
	}

}
