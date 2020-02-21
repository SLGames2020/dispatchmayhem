// the following code is an adaptation of the code presented on: 
// https://mariusbancila.ro/blog/2017/11/19/revisited-full-fledged-client-server-example-with-c-rest-sdk-2-10/

#include <cpprest/http_listener.h>
#include <cpprest/json.h>
#include <time.h>

#pragma comment(lib, "cpprest_2_10")

#include "GetRequestView.h"
#include "PostRequestView.h"

using namespace web::http;
using namespace web::http::experimental::listener;
using namespace std;

int main()
{
	//http_listener listener(L"http://localhost:8777/SLCGame311");
	//http_listener listener(L"http://149.248.59.60:8777/SLCGame311");
	http_listener listener(L"http://149.248.59.60:8777/DispatchMayhem");

	listener.support(methods::GET, handle_get);
	listener.support(methods::POST, handle_post);
	
	try
	{
		listener.open()
			.then([&listener]() { wcout << ("\nstarting to listen :: \n") << listener.uri().to_string().c_str(); })
			.wait();
	}
	catch (exception const & e)
	{
		wcout << e.what() << endl;
	}

	// infinite while loop to ensure our application continues to run and doesn't reach the end
	while (true);

	return 0;
}
