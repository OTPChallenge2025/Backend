    Technologies + How To Run:

        Project developed using C# / .NET 8.0. (and VisualStudio)


    Main purpose of project:

        BTCodeCrafters OTP Challenge is an app where you generate an OTP and use it for logging into an account.


    How it works:

        A GET method generates the OTP, and a POST verifies if the generated OTP is the correct one or not.
    The data is encrypted before it is transmited or received. After you generate an OTP, you cannot generate another for
    the time that was set before you generated it (I check if you did or did not try to generate another OTP).

        I save the decrypted OTP in a file (the OTP is encrypted during transmision, and decrypted after you receive it). I use a file
    instead of a database for simplicity, but the concept is the same. You can store the encrypted OTP in a database, and decrypt it
    whenever you want to check for a login.

        The Unit Testing is done in another project situated in the BE repository. The tests refer to the GET and POST methods mentioned
    before. To run the tests, you left click on 'Run Tests' when right clicking on the file 'OTPManagerControllerTests'.
