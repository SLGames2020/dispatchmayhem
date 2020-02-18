#pragma once

#include <cpprest/http_listener.h>
#include <cpprest/json.h>

using namespace web;
using namespace web::http;
using namespace std;

void handle_get(http_request request);