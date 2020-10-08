# Whistler
Pure C# PROOF OF CONCEPT Stealer that sends logs to PHP script that stores as txt

# Features
- Get chromium based browser logins (Chrome, Edge, FireFox, Brave etc)
- Get FileZilla logins (when there was no masterpassword)
- Get some CDKeys from games
- (Not implemented fully) Outlook
- (Not implemented fully) OpenVPN , ProtonVPN, NordVPN logins
- System Informations (IP , Country , PC Specs etc)

- Uploads to PHP Server that stores the logs as txt files.
- Logfiles are encoded with special lambda methods.
- Logfiles are uploaded invisible to Fiddler / Wireshark network analyzers
- Does not leave a trace on the filesystem when ran in mem or USB.
