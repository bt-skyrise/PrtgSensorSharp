# PrtgSensorSharp

_Work in progress_

The goal of this project is to facilitate creation of custom PRTG sensors,
especially _Advanced EXE/SCRIPT Sensors_.

## Creating Advanced EXE/SCRIPT Sensor

_More details comming soon_

1. Create new .NET console application and get _PrtgSensorSharp_.

2. Write a sensor. Make sure not to print anything into console!

```C#
public class Program
{
    public static void Main()
    {
        PrtgExeScriptAdvanced.Run(() =>
        {
            // Get some cool metrics...

            return PrtgReport.Successful(new[]
            {
                new PrtgResult("Number of connected users", connectedUsers),
                new PrtgResult("Number of active users", activeUsers)
            });
        });
    } 
}
```

3. Build it and put it in `Custom Sensors\EXEXML` directory.

4. Create _EXE/Script Advanced_ sensor in your PRTG 
instance and select the exe you just created.

5. Enjoy some fine metrics.

To learn mroe about PRTG and creating sensors, please visit:
* [PRTG website](https://www.paessler.com/prtg)
* [EXE/Script Advanced Sensor Manual](https://www.paessler.com/manuals/prtg/exe_script_advanced_sensor)
* [Custom Sensors API](https://prtg.paessler.com/api.htm?tabid=7)
* [PRTG demo instance](https://prtg.paessler.com)