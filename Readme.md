## Api Json Result for ASP.NET MVC applications
This package contains some magic titty sprinkles to help people who wish to return Json results for their ASP.NET MVC applications. Like .. an API website :)

## Code speaks.

* ```JsonErrorHandler``` to replace the normal ErrorHandler. This way, any errors -in code- are now outputted as json.
* ```ApiJsonResult``` this is the money shot. your model object is returned as some JSON, but in a standard way. Standard?? What's this standard you speak of?

### JSON standard
.. which really means, JSON response based on Api.StackExchange.com. If it's good enough for them, well .. it's good enough for me. And it should be good enough for you. I mean, seriously. The Stack-Krew are balltastic-smart. So just bend over and accept it.

## I said .. code speaks!

Oh yeah... 

1. Replace the global error handling.

```
public static void RegisterGlobalFilters(GlobalFilterCollection filters)
{
    //filters.Add(new HandleErrorAttribute());
    filters.Add(new HandleJsonErrorAttribute());
}
```

2. Create some ActionMethod and return some Json .. but in this api format!

o) Sending back a single object.

```
public ActionResult Index()
{
    var pewPew = new PewPew
                        {
                            Name = "Pure Krome",
                            Age = 999,
                            DanceMoves = new List<string>
                                            {
                                                "Melbourne Shuffle",
                                                "Moonwalk"
                                            }
                        };
    return new ApiJsonResult(new ResponseWrapper
                                    {
                                        Items = new List<object> {pewPew}
                                    });
}
```

o) Sending back a list of objects...

```
public ActionResult Index2()
{
    var dancingPeople = new List<PewPew>
                            {
                                new PewPew
                                    {
                                        Name = "Pure Krome",
                                        Age = 999,
                                        DanceMoves = new List<string>
                                                            {
                                                                "Melbourne Shuffle",
                                                                "Moonwalk"
                                                            }
                                    },
                                new PewPew
                                    {
                                        Name = "AssHat",
                                        Age = 999,
                                        DanceMoves = new List<string>
                                                            {
                                                                "Sprinkler",
                                                                "Small Box, Middle Box, Large Box"
                                                            }
                                    }
                            };

    return new ApiJsonResult(new ResponseWrapper
                                    {
                                        Items = new List<object> {dancingPeople},
                                        Page = 1,
                                        PageSize = 10,
                                        TotalItemsCount = 100,
                                        TotalPages = 10
                                    });
}
```

### Pull Requests? 
Awww yeah! I do accept em *hint thint*
