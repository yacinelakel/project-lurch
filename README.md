# project-lurch

An api for integrating towards different bot-systems.

Currently the api can connect to a telgram bot using telegrams webhook functionality.

**Under Construction**

# Development

### Use Ngrok to create a public url

In order to connect a telegram bot using a webhook, you will need a public url with ssl.
[Ngrok](https://ngrok.com/) is a tool that binds a generated public url given from ngrok to your local host.

Run the following command to create a public url that binds to `localhost:8443`.
```
ngrok http -host-header="localhost:8443" 8443
```

Note that the flag `-host-header` is required if you are using IISExpress. [(source)](https://www.twilio.com/docs/usage/tutorials/how-use-ngrok-windows-and-visual-studio-test-webhooks#using-ngrok-manually-with-a-visual-studio-hosted-aspnet-application)

### Bind the public url to the telegram webhook

Once you run your ngrok command, it will generate a public url that are forwarded to your localhost.

Copy the url starting with `https`.

To bind a url to the telegram webhook, run the following:
```
curl -F “url=https://<<NGROK_URL>>/<WEBHOOKLOCATION>" https://api.telegram.org/bot<YOURTOKEN>/setWebhook
```
