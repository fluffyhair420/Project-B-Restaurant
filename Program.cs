/*
 ## Setting up a new test project and running unit tests  ##
    1. The following instructions will create a new mstest project in a new folder (e.g. SampleTest):
        /> dotnet new mstest -o SampleTest
    2. Change the current directory to go into the newly created folder for the mstest project:
        /> cd SampleTest
    3. Add a reference to the project which contains the source code you want to test (e.g. a console application named SampleProject):
        /> dotnet add reference ../SampleProject/SampleProject.csproj
    4. Execute the test command:
        /> dotnet test

    ##  Test attributes ##
    Some examples of test attributes are:
    - [TestClass]
    - [TestMethod]
    - [DataTestMethod]
    - [DataRow]
    See the CheatsheetTest.cs file for more examples of test attributes.

    ## Asserts ## 
    Some examples of Assert methods are:
    - Assert.AreEqual
    - Assert.IsTrue
    - Assert.IsNull
    - Assert.IsNotNull
    See the CheatsheetTest.cs file for more examples of Assert class methods.

    ## Data-driven unit tests ##
    The following methods are examples of data-driven unit tests:
    - TestAddSongs
    - TestAddSongsDynamicDataMethod
    See the CheatsheetTest.cs file for more examples of Data-driven unit tests.
*/
