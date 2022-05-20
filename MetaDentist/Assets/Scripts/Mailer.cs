using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Net.Mail;
using UnityEditor;
using System.IO;
public class Mailer : MonoBehaviour
{
    // Start is called before the first frame update
    public void mailer()
    {
        
        string path = "Assets/result.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log("BERKE");

        string file = reader.ReadToEnd();
        Debug.Log("file" + file);
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.Credentials = new System.Net.NetworkCredential(
            "medsimproject@gmail.com",
            "medsim22");
        client.EnableSsl = true;
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress(
              "medsimproject@gmail.com",
            "medsim22",
        System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress("medsimproject@gmail.com");
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = ".....  " + file; //TODO: Kullanýcý VERÝLERÝ  + reader.ToString()
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "..... Kullanýcý Sonuçlarý";
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        string userState = "test message1";
        client.SendAsync(message, userState);
        reader.Close();
    }
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Debug.Log("Send canceled " + token);
        }
        if (e.Error != null)
        {
            Debug.Log("[ " + token + " ] " + " " + e.Error.ToString());
        }
        else
        {
            Debug.Log("Message sent.");
        }
    }
}



