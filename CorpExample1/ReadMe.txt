Run from VisualStudio, or extract "publish.zip" and run the executable, CorpExample1.exe (.net core 3.0 installation required on the machine - tried including framework-independent installation,
	but github does not accept files larger than 25 MB).

Once launched, the app will be listening on https://localhost:5001 . Expected (relative - i.e. https://localhost:5001/api/Agents ) URLs / request types are:
	GET		/api/Agents					- returns list of all agents 
	POST	/api/Agents with json payload, i.e. {
													"name":"Max Ye",
													"tier":1
												} - adds a new agent
	GET		/api/AgentDetails/id		- returns all agent details by agent's int ID
	PUT		/api/AgentDetails/id with json payload, i.e.
												{
													"_id":1988,
												  "name":"Max Ye",
												  "tier":1,
												  "address":"101 Some Street",
												  "city":"Some City",
												  "state":"CA",
												  "zipCode":"95000",
												  "phone":{
															"primary": "206-221-2345",
															"mobile": "206-555-3211"
														}
												} - updates any/all fields
	GET		/api/Customers/agentId		- returns list of agent's customers in specified format
	POST	/api/Customers with json payload, i.e.
												    {
														"agent_id": 467,
														"isActive": true,
														"balance": "$3,613.23",
														"age": 36,
														"eyeColor": "brown",
														"name": {
															"first": "Maxim",
															"last": "Yev"
														},
														"company": "EXODOC",
														"email": "someemail@exodoc.us",
														"phone": "+1 (842) 497-3565",
														"address": "175 Malbone Street, Sacramento, California, 95670",
														"registered": "Saturday, June 2, 2018 6:03 PM",
														"latitude": "-35.784215",
														"longitude": "-17.864023",
														"tags": [
															"et",
															"anim",
															"reprehenderit",
															"reprehenderit",
															"sint"
														]
													} - adds new customer
	DELETE	/api/Customers/guid			- deletes a customer by guid
	
	GET		/api/CustomerDetails/guid	- returns all customer details by guid
	PUT		/api/CustomerDetails/guid with json payload, i.e.
													    {
															"agent_id": 467,
															"guid":"6b71c8c6-f87f-4518-b277-ee81fc9c24ed",
															"isActive": true,
															"balance": "$3,613.23",
															"age": 36,
															"eyeColor": "brown",
															"name": {
																"first": "John",
																"last": "Doedoe"
															},
															"company": "EXODOC",
															"email": "someemail@exodoc.us",
															"phone": "+1 (842) 497-3565",
															"address": "175 Malbone Street, Sacramento, California, 95670",
															"registered": "Saturday, June 2, 2018 6:03 PM",
															"latitude": "-35.784215",
															"longitude": "-17.864023",
															"tags": [
																"et",
																"anim",
																"reprehenderit",
																"reprehenderit",
																"sint"
															]
														} - updates any/all of customer's details
