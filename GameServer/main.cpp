// the following code is an adaptation of the code presented on: 
// https://mariusbancila.ro/blog/2017/11/19/revisited-full-fledged-client-server-example-with-c-rest-sdk-2-10/

#include <cpprest/http_listener.h>
#include <cpprest/json.h>

#include <iostream>
#include <map>
#include <string>
#include <set>

#include "Resources/Character.h"

#pragma comment(lib, "cpprest_2_10")

using namespace web;
using namespace web::http;
using namespace web::http::experimental::listener;
using namespace std;

Character* myMan;
int randValue;
map<string, int> LeaderBoards;
map<string, int> Sessions;
vector<string> TheQ;

int g_SessionID = 0;

void display_json(
	json::value const & jvalue,
	utility::string_t const & prefix)
{
	wcout << prefix << jvalue.serialize() << endl;
}

//Derived from: https://stackoverflow.com/questions/27720553/conversion-of-wchar-t-to-string
string ConvertWCharToString(wstring textToConvert)
{
	// your new String
	string str(textToConvert.begin(), textToConvert.end());

	return str;
}

bool CheckRequest(string request, string APIstr)
{
	return (strncmp(request.c_str(), APIstr.c_str(), APIstr.length()) == 0);
}

wstring StringToWString(const std::string& s)
{
	std::wstring temp(s.length(), L' ');
	std::copy(s.begin(), s.end(), temp.begin());
	return temp;
}
/* Walkthrough 1: create HandleGet function and strip out headers and requests
- json node handling
- json serialization of Character object
- security of server

// walkthrough 2: handle individual requests in new classes for each.
- compartmentalization of code
- security of server through headers

// walkthrough 3: strip out common functionality required for a request into a base request class.
- compartmentalization of code
- inheritances
*/
void handle_get(http_request request)
{
	cout << "\nhandle GET\n";

	// JD NEED WCOUT OR IT PRINTS INCORRECTLY
	string APIpath = ConvertWCharToString(request.absolute_uri().to_string());
	cout << "API: " << APIpath.c_str() << endl;
	if (strncmp(APIpath.c_str(), "/restdemo", 9) == 0) // match for first 9 character
	{
		// now find our which request we should call based on the rest of the APIpath
		string RequestPath = APIpath.erase(0, 10);
		cout << "RequestPath: " << RequestPath.c_str() << endl;

		string APIstr = "GameOver";
		if (CheckRequest(RequestPath, APIstr))
		{
			// do waka waka request
			json::value JSONObj = json::value::object();
			JSONObj[L"GameOver"] = true;
			request.reply(status_codes::OK, JSONObj);
		}

		APIstr = "PlayerDied";
		if (CheckRequest(RequestPath, APIstr))
		{
			// get the headers from the request
			if (request.headers().has(L"UserID"))
			{
				// do waka waka request
				json::value JSONObj = json::value::object();
				JSONObj[L"UserID"] = true;
				request.reply(status_codes::OK, JSONObj);
			}
			else
			{
				display_json(myMan->asJSON(), L"R: ");
				request.reply(status_codes::FailedDependency, L"Error, missing UserID");
			}
		}

		APIstr = "getServerRand";
		if (CheckRequest(RequestPath, APIstr))
		{
			json::value JSONObj = json::value::object();
			JSONObj[L"RandVal"] = randValue;
			request.reply(status_codes::OK, JSONObj);
		}

		APIstr = "getCharStats";
		if (CheckRequest(RequestPath, APIstr))
		{
			// do getCharStats request

			// get the headers from the request
			if (request.headers().has(L"Token"))
			{
				// get the userID
				web::http::http_headers reqHeaders = request.headers();

				string val = ConvertWCharToString(reqHeaders[L"Token"]);
				cout << "Token: " << val << endl;
				if (strcmp(val.c_str(), "Quijibo") == 0) //  VALIDATION PASSED, they know the password.
				{
					cout << request.body();

					display_json(myMan->asJSON(), L"R: ");
					request.reply(status_codes::OK, myMan->asJSON());
				}
				else
				{
					display_json(myMan->asJSON(), L"R: ");
					request.reply(status_codes::Forbidden, L"Error, unauthorized token");
				}
			}
			else
			{
				display_json(myMan->asJSON(), L"R: ");
				request.reply(status_codes::FailedDependency, L"Error, missing token");
			}

			// get the body of the request
		}
	}


}


/*void handle_request(
http_request request,
function<void(json::value const &, json::value &)> action)
{
auto answer = json::value::object();

request
.extract_json()
.then([&answer, &action](pplx::task<json::value> task)
{
try
{
auto const & jvalue = task.get();
display_json(jvalue, L"R: ");

if (!jvalue.is_null())
{
action(jvalue, answer);
}
}
catch (http_exception const & e)
{
wcout << e.what() << endl;
}
})
.wait();


display_json(answer, L"S: ");

request.reply(status_codes::OK, answer);
}
*/
void handle_post(http_request request)
{
	cout << "\nhandle POST\n";
	//cout << "\nhandle GET\n";

	// JD NEED WCOUT OR IT PRINTS INCORRECTLY
	string APIpath = ConvertWCharToString(request.absolute_uri().to_string());
	cout << "API: " << APIpath.c_str() << endl;
	if (strncmp(APIpath.c_str(), "/restdemo", 9) == 0) // match for first 9 character
	{
		// now find our which request we should call based on the rest of the APIpath
		string RequestPath = APIpath.erase(0, 10);
		cout << "RequestPath: " << RequestPath.c_str() << endl;

		string APIstr = "Login";
		if (CheckRequest(RequestPath, APIstr))
		{
			if (request.headers().has(L"Content-Type"))
			{
				json::value temp = json::value::object();
				request.extract_json()       //extracts the request content into a json
					.then([&temp](pplx::task<json::value> task)
				{
					temp = task.get();
				})
					.wait();
				//do whatever you want with 'temp' here //temp contain all the json stuff

				//getting the value of each key:
				if (temp.has_string_field(U("Name")))
				{
					utility::string_t Name = temp.at(U("Name")).as_string();
					//int score = temp.at(U("Score")).as_integer();

					json::value JSONObj = json::value::object();
					string initials = ConvertWCharToString(Name);


					Name.insert(0, L"Hello ");
					if (Sessions[initials] != NULL)
					{
						Name.append(L" welcome back.");
						JSONObj[L"SessionToken"] = Sessions[initials];
					}
					else
					{
						g_SessionID = rand();
						Sessions[initials] = g_SessionID;
						Name.append(L" welcome to the server. Since this is your first time, please see your authentication token attached.");
						JSONObj[L"SessionToken"] = g_SessionID;
					}

					JSONObj[L"ResponseStr"] = json::value::string(Name);
					request.reply(status_codes::OK, JSONObj);
				}
				else
				{
					request.reply(status_codes::BadRequest, "Invalid JSON Request");
				}
			}
			else
			{
				request.reply(status_codes::BadRequest, "Invalid Request type");
			}
		}

		APIstr = "PostScore";
		if (CheckRequest(RequestPath, APIstr))
		{
			if (request.headers().has(L"Token"))
			{
				json::value temp = json::value::object();
				request.extract_json()       //extracts the request content into a json
					.then([&temp](pplx::task<json::value> task)
				{
					temp = task.get();
				})
					.wait();
				//do whatever you want with 'temp' here //temp contain all the json stuff

				//getting the value of each key:
				utility::string_t Name = temp.at(U("Name")).as_string();
				int score = temp.at(U("Score")).as_integer();

				string initials = ConvertWCharToString(Name);
				if (LeaderBoards[initials] < score)
				{
					LeaderBoards[initials] = score;
					request.reply(status_codes::OK, "new high score!");
				}
				else
				{
					char buffer[200];
					sprintf_s(buffer, "Sorry too low, the previous high score was: %d", LeaderBoards[initials]);
					request.reply(status_codes::OK, buffer);
				}
			}
			else
			{
				request.reply(status_codes::BadRequest, "missing session token");

			}
		}

	}
}

void PrintListening()
{
	cout << "\nstarting to listen\n";
}


int main()
{
	http_listener listener(L"http://10.105.156.53/");

	myMan = new Character("Jimmy Joe Joe Shabadoo");
	srand(time(0));
	randValue = rand();
	g_SessionID = rand();

	listener.support(methods::GET, handle_get);
	listener.support(methods::POST, handle_post);

	// this is asynchronous, it spawns a new process using multi threading.
	/*pplx::task<void> listenTask = listener.open();
	listenTask.then(PrintListening);
	listenTask.wait();*/

	try
	{
		listener.open()
			.then([&listener]() { wcout << ("\nstarting to listen :: \n") << listener.uri().to_string().c_str(); })
			.wait();
	}
	catch (std::range_error const e)
	{
		wcout << e.what() << endl;
	}
	catch (exception const & e)
	{
		wcout << e.what() << endl;
	}
	// infinite while loop to ensure our application continues to run and doesn't reach the end
	while (true);


	return 0;
}