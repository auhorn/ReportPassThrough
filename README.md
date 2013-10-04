##Report Pass Through (Proof-of-Concept) ##

This is a proof-of-concept...

###What is the goal###

We needed to achieve the Download of an Excel report for users. The user would not be able to access the report server directly and esp. not be able to modify the URL with containing report parameter. The web application should contain on a link to report


###Solution###

Create the report on the reporting server secured by an username / password combination that is only available in the application. The application itself implements an `HttpHandler` retrieving the Excel report from the reporting server and pass it to the user.  