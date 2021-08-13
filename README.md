# JokeApi
This is my version of the Joke API assignment.

The public azure URL is: https://philipgeil-jokeapi.azurewebsites.net/

The project is build to provide a random joke, within the selected category.
Each category requires permission given with an API Key
I have made 3 easy test keys

### Level 1
#### Permissions
* Dad Jokes
#### Test api key
test

### Level 2
#### Permissions
* Dad Jokes
* Blonde
* Knock knock
#### Test api key
test1

### Level 3
#### Permissions
Full permissions
#### Test api key
test2

## Language
The language supported is either danish or english. <br>
it is being set byt adding the `Accept-Language` to the request header. <br>

For danish the accepted values are: `DA` `da` `dk` `DA-dk` <br>
Any other value will return english.

## API Key
### Generating a new api key
You can create your very own api key, with the desired permissions
As mentioned above, there are 3 levels of permissons.
API key are provided in the following way:
https://philipgeil-jokeapi.azurewebsites.net/api/apikey?permissions=level1 <br>
`Where level1 can be replaced with either level2 or level3`

## Getting the categories
All the categories can be listed by the following url: 
https://philipgeil-jokeapi.azurewebsites.net/api/category

## Getting a random joke
The default category is Dad Jokes.
If you want to change the category you will have to provide the `?categoryid=` query. <br>
`There are 4 categories so the choices are 0, 1, 2, 3` <br>
And also the API Key needs to be in the url with the `?key=`query - an example might look like this: <br>
https://philipgeil-jokeapi.azurewebsites.net//api/joke?key=std+FyzvljsM/MGhP2co2vJN72imWrVDvDZujBzp2o4=&categoryid=3 <br>
If categoryid is not provided, it either takes your last choice, which is saved in a cookie, or it defaults to 0.


#### Maybe a website will be made at some point?...
