using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using webapp.Models;

namespace webapp.Data
{
    public static class MvcDbSeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            await InitIdentity(serviceProvider);
            await InitContent(serviceProvider);
        }

        public static async Task InitIdentity(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            // Add Roles
            string[] roleNames = { "Administrator", "Editor", "User" };
            foreach (var roleName in roleNames)
            {
                IdentityResult roleResult;
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new AppRole(roleName));
                }
            }

            // Add AppUsers from appsettings.Development.json
            var users = configuration.GetSection("AppUsers").GetChildren();
            foreach (var user in users)
            {
                var appUser = new AppUser
                {
                    UserName = user["Email"],
                    Email = user["Email"]
                };

                var _user = await UserManager.FindByEmailAsync(appUser.Email);
                if (_user == null)
                {
                    var createUser = await UserManager.CreateAsync(appUser, user["Pass"]);
                    if (createUser.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(appUser, user["Role"]);
                    }
                }
            }
        }

        public static async Task InitContent(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            using (var context = new MvcDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcDbContext>>()
                    ))
            {
                // Return if Items exist
                if (context.Item.Any())
                {
                    return;   // DB has been seeded
                }

                var email = configuration.GetValue<String>("AppUsers:administrator:Email");
                var _user = await UserManager.FindByEmailAsync(email);

                context.Item.Add(
                    new TemplateItem
                    {
                        OwnerId = _user.Id,
                        Keyname = "template-one",
                        CreateDate = DateTime.Now,
                        ModificationDate = DateTime.Now
                    }
                );
                context.SaveChanges();

                context.Template.Add(
                    new Template
                    {
                        Id = 1,
                        Keyname = "template-one",
                        Title = "Template One",
                        TemplateStatus = "Public"
                    }
                );
                context.SaveChanges();

                context.Item.AddRange(
                    new ContentItem
                    {
                        OwnerId = _user.Id,
                        Keyname = "ascii-einstein",
                        CreateDate = DateTime.Now,
                        ModificationDate = DateTime.Now
                    },
                    new ContentItem
                    {
                        OwnerId = _user.Id,
                        Keyname = "ascii-hand",
                        CreateDate = DateTime.Now,
                        ModificationDate = DateTime.Now
                    },
                    new ContentItem
                    {
                        OwnerId = _user.Id,
                        Keyname = "ascii-carrot",
                        CreateDate = DateTime.Now,
                        ModificationDate = DateTime.Now
                    }
                );
                context.SaveChanges();


                //context.ItemUser.AddRange(
                //   new ItemUser
                //   {
                //       ItemId = 1,
                //       UserId = "2",
                //       Role = ItemUserRole.Editor
                //   },
                //   new ItemUser
                //   {
                //       ItemId = 1,
                //       UserId = "3",
                //       Role = ItemUserRole.Editor
                //   },
                //   new ItemUser
                //   {
                //       ItemId = 2,
                //       UserId = "1",
                //       Role = ItemUserRole.User
                //   }
                //);
                //context.SaveChanges();

                context.Content.AddRange(
                    new Content
                    {
                        Id = 2,
                        Keyname = "ascii-einstein",
                        TemplateId = 1,
                        ContentStatus = "Approved",
                        Title = "ASCII Einstein",
                        Text = @"<pre>MmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmN
Mhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhyyyyyyyyyyyyhyyyhyhhhhhhyyhhhhhyyhhhhhhhyhyyyyyhyhyyhhhhhhhhhhhhhhhm
Mhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhyyyyyyssyysyyyyyyyyyyyyyyyyhyyyyyyhhhhyyyyyyssyyyyyyyhhhhhhhhhhhhhhym
Mhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhyyyyosoooo+oosssssyyysyyyyyyyyyyyyyyyyyyysssssssyyyyyyyhhhhyhhhhhhhym
Mhhhhhhhhhhhhhhhhhhhhhyyhyhyyyysso+oo/:/+/+//++++++soooooososysyyyyyysso/++soooyyyyyyyyyyyyyhhyhhhym
Mhhhhhhhhhyhhhhhhhhhhyyyyysssss++/://+/:////://////++++++oosoysyyssoso+//++oo+ossyyyyyyyyhhhhhhyhhym
Mhhhhhhhhhyyyyyhyhyyyyyyyso/+++/+:/-::---:--::/::/+///o+oosssooso+++++//+++oo++osssssyyyyyyyyhhyhhym
Mhhhhhhhhhsysosysoososss+/+//::::-..---.-::-::////oo//ossosssso+//::/+//+++oo+oossysyyyyyyyyyhhhhhym
Mhhhhhhhhyssso+/:/-++o/////:/:::--...--:-/:-/+/+/ooo+osssssssso++/-:/+++++++ooossyyyyyyyyyyyyhhhhhym
Mhhhhhhyyyhyyyo+o++++o++//:::----..--::::////++++o+ooyssssssssso+::/+++++ooooossssyyyyyyyyyyyyhyhyym
Mhhhhhyhhhysoosss/////+//:::-..----//::://://++oooosssssooosssoo+//++++ooooooosssssyyyyyyyyyyhhyhyym
Mhyhhhhhyys/::///-::/::::::::--://///////////+++o+ssoo++oooooooo+++++++oo+osooooosyyyyyyyyyyyyyyyyym
Mhhhyyyyys+:::::::--:-.--.--:///////////////+oo++soo+//+oo+ooooooo++ooosooooooooossyyyyyyyyyyyyyyyym
Myyyhhyho/:-..----.......-----::////://:/:/+oo+oosoo+++oooooosooooo+oosooososoooossyyyyyyyyyyyyyyyym
Mhhhhhhyo+:-`.---....----.----:://++/+////+++oooossoo+oo+ooosssoooooosooosssssosssyyyyyyyyyyyyyyyyym
Mhhhhhhyyo-.------.-::--....-//:-/+o+++++++++oooosooooooooossssssooo++++ooooosssssyyyyyyyyyyyyyyyyym
Mhhhhhhhyso:///:::-::::-...-::-.-:+ooooo+++++oooosssoooooossosoooooo+++o+++osssyyyyyyyyyyyyyyyyyyyym
Mhhhhhhhys/:.-////..-....---.```-/oooossooo+ooooossssoooooossoooooo+oo++++ossssyyyyyyyyyyyyyyyyyyyym
Mhhhhyyys/::-/++/:-```......```..-/++osssooooossssssssoooosssssoooooo+++ossssssysssyyyyyyyyyyyysysyd
Mhhhhyyyo-:.`.-----`````.--..``.-:/osssssssssssssssssoooossosssssooooooooosssssssysyssssssyysssooood
Mhyyhyyys:.``.-:-:.````.-:++:::-:/+osssssssssssssssssoooooosossosssoooooossssssssssyyyyyyyyyssssssoh
Mhyyyyyyo:....---.`````-/+/-:::::/+ossssssoooooooooooooooooooosoooossossssssssssossyyyyyyyyyyyyyyyyd
Mhyyhyso+-://:-.```````:/+//---//+ossoooooooooooooosooooooooooosoooooooosssssooosssyyyyyyyyyyyyyyyym
Myyys+:/://o+/-.``````.:+//+//+++osoooooooo++oooooo++o+o++ooooooooooossoooossssoooossssssyyyyssyyysd
Myyhoooooo///so+.````.-++++/:::osssooooo++++++oooooooo++/+++++oooooooossssssoosssooossssssyyysyyysod
Mhyyyyo++/-++oso-`..-/oo//:+oo+oooooo+++++++++o++++++++++++++++ooooooooosssossssssoooooosyyyyyyyyssd
Mhyhyy++:-+//oso+-.-/oo:--+syyyyssooo+/++++o++o+++++++////++++oooooooooooosoossssssooooosssysyyyyyyd
Mhyhyy+++/o//oss+o`-/+:.:+oossyyyyyso+////++oo+++++/+/++//++++++oooooooosooossoooosooosssyyyyyyyyyyd
Mhyyyy/://++/ooooy.-:::/ssssssssyysss+/:/::/++++++++++//:///++oooooooooooooooooooosooo+ossyyyyyyyyym
Mhyyyys+/////+++so..--+oyysyysssssssoo//:--///+++oo++//+++/+++++ooooooooooooooooooooosssssyyyyyyyyym
Myyyyyyyo/-:://:o/-.-/ossyyyyyssosssso++/:::////:o++//+//://+++++o+oooooooooooooo+++++oosssyysysyyym
Myyyyyyyo-:+++++:.--:osyysssoyyysosssso++///::///+++/++++/////+++++ooooooooo+ooooo+++/+ossssyyyysyyd
Myyyyyyyoo+://++.-:/+oososooossso:+sosys+++/////+++oossssssoooo+oo+ooooooo++++ooo++++++ossssyyyyyssd
Myyyyyyyso+:-::.--/+++++:osss++/::/++syyo//+/+++oooooosssyssssssoo+ooooo+++++++++++++++oossyssssyysd
Myyyyyyyys+-::-.:-++/+o++oooooooo++/oyys+:/++ooooooossssoooooo+oo++oooo+++/+/+++++++++++++ooosssyyyd
Myyyyyyyyyooo+::::+++oooo+o+++++++osyys+:://++oooosssyysoooss+++oo+ooo+++++++/+/+++ooo+ooosossyyyyyd
Myyyyyyyyyosss/:-:+ossoooo++++ooosyyys+/:////++/++o/+yysysooosooooooo++++++///+/++++++++oooooosyyyym
Myyyyyyyyyysso---+osssooooo+oooosyysso///////+:++o+//ooo++ssoooooooo++++++/////++++++++++oooossyyyyd
Myyyyyyyyyyysy--:+sssssossoo+oosso//+/:://///+/++ooo+///:/+oooo++++++++++////+++o++ooo++/+ossssyyyyd
Myyyyyyyyyyyys:-/oss+:--/++++oos:``./:-//////+///++++++++++/++++//////++++++oo++ooooooo///ooossyyssd
Mhyyyyhyyyyyys::/o+:..-:/+///oso``-:::://////++:/+++/:/::://////////++o+++++++++ssssoooo++oosssysssd
Myyyyyyyyyyo:`-:+/-.-////:/+o:.`-//:::://+///////:////::::::://://++ooooo+/:::/ooossosoooooosssysssd
Myyyyyyyys:  `-::--/++/::/sy-`.://-.-:://++++/::://:/:::::::////+++ooooo+///:+ss+ssssoosssossssysssd
Myyyyyyy+.   -:---/oo///++syo-:+/:-...-://////::::::/::/:::://++oooooo++:://oys/sssssoosssssysyyyysd
Mo+/:::.    ./:::/ooo/:://+syy/o+:--.-://::/://:::::::////////++oooooo+:-:/syy+oyssssoooosssosyyssoh
M-.-::.     -//o/ssoo.:/++/++oooo++///+/+/:--:/::::://////+/+++ooo+oo+/::/+ysoosssossoososoooosssood
M::..```   .:+ososss+/+oooo+++++oooo+/+/:-..-://:::://////+/++oo+oooo+//+osysooosysoosoosoooosssssod
M:.````   .//ossyssssssysyysooooo+o+++o/:---:::/::::///////++oo++oo/ooosoososososooo+o+ooo+oooosoooh
M-`.`  `  `://yoyysooyyyyyyyyssossssooo+/::::/://::://////++oooooo-`/oooossoooooooo++o+ooo++o+++osoh
M-`-  ``   `:/sssyoosysssysssssosssyssso++//::--/:////////+ooooo+.``.+ossoosooooo+++/++oooo+ooosossd
M-`:  ``    ./oyyyssyssoossssoo+osssyysssooo++:://///+///+ooooo:.````:+//+ooo++++++o+/+ooooooosososd
M-.:  `     `.+yyyssssoooooooooo+++/oyyyyyoso++////+++//++ooo+.``````.++/:-::/:/+oooo+/+ooosososyysd
M-.:  `..``  `-syyyyooo+o+///+++o++/oyyyyyy/+++//+++++/++ooo+.`.``````:+++++////+/oooooo+ooooosyssoh
M--. ``--``  `./yyyyyoooo++/::-:----oyyyyyyo++++++++++++o+o/``.``````./++++++o++o+++oosso++sosssossd
M-.-```.````  `-oyyyyyssoo+///:-..../yyyyyyyooooooooo+oo+o:``-.``````:+++++++oo+ooooooooooosssooyyym
M/-:````` ``` `../syyyyysoo++++/:---:yyyyyyyysooooooooooo-``-..`````-///+++++o+oooooooooooossssosyym
M+:-``  ` ````.-::-/syyyyyssoooo+/::+yysoyyyysoooooooos+.``..``````-////+++++ooooooooooossssssysoosd
M///:.`     ````.//:.:+osssssyyyssssyys+/oyyyyooooo++o/` `.```````-://////++o++ooooooooooossssssssod
M+////+:.`   `.``.++:``./+//++syyyyyysso++syyyy+//:-+-   ```````.://////+//+o+++++++oooooooosssssssd
M++//++++/:-.`````.:+/:.`-::/+osyyo+////:--yyyyo:.-+.  ```````.:://///////+o++++++++++ooooosoosssysd
M+/+/+++++/+/////:-...:::-..:/+syyso/:::--./yyyy/:/`   ````.-:///////////+++++/+/+++++++ooooosossssd
M+//++++o+///+++++oo+//::-:/:-:osyss+/:--..:syyyy:`.` `..-//////////////+++++++////++++o+++oooosossd
M+///+/++/+/++o++++++++++/+/:/:-::+ososo//+::yyyys::/::/+oooo+////:::///////+///////++o+++++oooooosd
M+:+//+++++++++++o+++++++///://://osyyyysosooyyyyyy///oyyyyyyyso/::://:+++//////////+ooo++++oooooood
M/:/++++/+/++++++++++++/////://:/++syyyyyyyyyyyyyyyssyyyyyyyyyyys//://+++/+/////////+ooo+o+++ooooood
M/-::://++++++o+o++++o/////////://+oyyyyyyyyyyyyyyyyyyyyyyyyyyyyyo+/+/++++/++//////++soo++++++oooooh
M+//+//+/++o+o++++++++++o////////+ossyyyyyyyyyyyyyyyyyyyysyyyyyyy+//+/+++/////////++oso++++++++ooooh
M+///+++++++++++/++++/+++/+/+//++osyyyyyyyyyyyyyyyyyyyyyyyyyyyyys///++++++/++//////+osoo++++++oooood
M+/+++++++++++/+++oo+++++++++++++osyyyyyyyyyyyyyyyyyyyyyyyyyyyyo//+++o+++o+++++/::+oys++++++++ooosod
M+++o++++o+++++++ooo++o++++++++++ossyyyyyyyyyyyyyyyyyyyyyyyyys++//++oo+++++/++///++ssoo++++++o+ooood
Mssossssssosssssyssssssysssssosssssyyhhhhhhhhhhhyyhhhhhhhhhyssssssssysyssssssooossshhssssssssssysysd
</pre>"
                    },
                    new Content
                    {
                        Id = 3,
                        Keyname = "ascii-hand",
                        TemplateId = 1,
                        ContentStatus = "Complete",
                        Title = "ASCII Hand",
                        Text = @"<pre>
                                                           ``
                                                         ./hs+/-
                                          .:sso:.       +yddsyy+
                                         /syyyhyo       +yddhmh+
                                        `+syhoss+      /yhyydhy+
                                        +osyhsyo-      +yyosys/.
                                        yyyyyso:       syysohy/`            `
                                        yoosyos:       -+ssoys:         `-shso+/
                                        -:+yyh+.        +o+/:.`         -omyhddh`
                                        `.--..`        /ossos-          :yhdhhhy `
                                      `-/+/:/:`        s+osyo.         `/syhsyhy `
                                      `/hyo/:`         //+os.          :sdyhyss+`
                                      `/hssss          ..:+s           +syyyys+.
                                      -dmyyy/          ::-`.          `/osyhy/`
                                      .yNNy/           --.            ++++-y-`
                                     .+mmmds`        .+ddo-`          +sys/-`             `
                                     .+myhyo        -smMNds/          osyy/            ``
                                     .+msoo/        :yhhhy+`         `o+o+                `
                                     `-o:--.        /yssso:         .+s/-`
   `/ ```                            `` .``         -o+oso:        .:oo-`                 .````
  `-s:oss/`                                            ```       `/oosh-                  /+ss:```
  `-sssssoyo:                                                    smNNNy.                .syyhddo-`
   .shys+/sys/                                                  `ohmmds.               -+hhsyh+y-
   .+ssoossss+                      ``.oos+---`                 `:+ooo                /syyyhmm+`
    `oyyyyosyo                      ./oodyoosho/:``````            ``                 +yyohdy+`
    `yyyyyymh+                     /ohmdMNdhmdyyyyyosyy`                              :yNmds:
     oyhdNhmo`                     +ddhNMNdyshhhhhsdmmdo+:---`                      .+s+/o.`
     `shdmys:                      ydysMNdhyyyddyydmdyshhddys/.        `           `.hds:
      `:oy+.                       .osshyyyyosyyosmdmmsshmNm/syo`.    `           :so/-`
        `.                         +/::/ossssshy+:hmdhoyshNm+ys:.`  ` `          -++.`
                                   -:.  `-+ossso++smdyysyysohoss-.``            -`
                                  ./-` `..:oo+o+/ooydhsdsyy+yyyhyo-`
                                 `.`-.   ```-`---.-///s+o+/odyyyhy+.
                        ```   `   . `          .-.  `.+:--+mhssyoyo:``
                    ```./+/.                          ` `-/+osoos/.` `-.``
                 `-:+++/oo-`   ``                        `-+//+os:..::/ss+-
               `-+hhyysos/-`   ``                         `.``-os.-+yhhhyos+`
               yhmNmddhyyyo-                               ```-/s:sysyhhyyy/`
               sydddddhyhy/// . ``                      `..`/-:/+:+oo/ssooo.
              `hddhhhhyyhyso+--``` `                `.:-:::./:///+///:o:.``
             .ohyhhdyddysy++o+:.`.`    `         ``.:+:o//+++/+osoo+/.-`
             `-yhhhhhyhmdsysoo//:-/    `  .-    .//sys/o++/o:oo+/oo/:-
               /shdhdhyhdyyyyhoo/-+`` `-..--   :syyysosssssyysoo+//::/
                /syhmdyyyyhhyhyhs+s..-::----`.:oyyddhyyhhyyyhssss/:-`
                `/hmNdhhNdhhhyssssyoo/:o-///oooshyNdhhhhhysyhs+/:```
                 ./ssmmdNhhhhdyyysoo+ooooossoooyddmmmmhddhyyhs+/.
                   `:/shdhyhhyyyyss.:osoyhhyyohmMMdmdhhmdhss+++:.
                      :syyyyso+yyo:`/os/shhhNhdmNNNdddhyhysoo++:
                      `.-..-/:.+s+:`/+sh:hddNNdddmMddhhhyys/:.`.
                                  ` ```/ +hdyMNmmdmhyhyyyo:``
                                        `:ohhhmmmhhhyss+:.``
                                         .-/sdhddhys+:`
                                          `.--oyy++-`
                                             `...``
</pre>"
                    },
                    new Content
                    {
                        Id = 4,
                        Keyname = "ascii-carrot",
                        TemplateId = 1,
                        ContentStatus = "Hidden",
                        Title = "ASCII Carrot",
                        Text = @"<pre>
                                                                                 .ymms``+ommh:
                                                                                -ymmmm-hmmmmmo `...
                                                                              -:mmmmmmydmmmmmhshddds
                                                                            -yddmmmmmmmmmmmmmmmmmmms
                                                                           odmmmmmmmmmmmmmmmmmyo:..
                                                                        -/ydmmmmmmmmmmdddoooo:`
                                                                      `/dmmmmmmmmmdhho.``
                                                                  .oosddmmmmmmdhhs-``
                                                              .+ssdmmmmmmmmmmmdssssssssssssssss/---
                                                         `-/sshmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmddddy
                                               `---`    /hddmmmmmmmmmmmmddysssssssssssssssssssshddds
                                            `:oosssoooshmmmmmmmmmmddddy/..`                    `...
                                           -osssssssssdmmmmmdhs+++....`
                                         `:osssssssssssyhhhyo-
                                       `:osssssssssssssssssss/`
                                     `:ossssssssssssssssssssss-
                                  `.-ossssssssssssssssssssssss-
                               `-++ssssssssssssssssssssssssss/`
                              `/ssssssssssssssssssssssssssss+.
                           `-:+sssssssssssssssssssssssssss+-`
                          .+ssssssssssssssssssssssssssso--
                        ./+ssssssssssssssssssssssssso+-
                     ..+ssssssssssssssssssssssssso+:`
                 `./+sssssssssssssssssssssssso//:`
               ./+osssssssssssssssssssssssoo/`
            `:/ossssssssssssssssssssoo+:::.`
         `:/osssssssssssssssssssooo-
      `:/osssssssssssssssso++++:
  `-//osssssssssssssss+/..`
 `+ssssssssssssssss+-.
-osssssssssssso///-
ssssssssss/::`
</pre>"
                    }
                );
                context.SaveChanges();



            } // end using(context)
        } // end InitContent
    } // end  class
} // end namespace
