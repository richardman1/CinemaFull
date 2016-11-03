namespace IVH7_Cinema.Domain.Migrations {
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using IVH7_Cinema.Domain.Entities;
    using IVH7_Cinema.Domain.Concrete;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Text;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    [ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<IVH7_Cinema.Domain.Concrete.EFDbContext> {

        private int IDForSeed = 1;


        public Configuration() {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(IVH7_Cinema.Domain.Concrete.EFDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            try {
                // Create cinemas
                context.Cinemas.AddOrUpdate(c => c.Name,
                    new Cinema() { Name = "Breda", Address = "Cinemalaan 12", ZipCode = "4823AB", City = "Breda", Phone = "0765666666", Email = "info@cinemabreda.nl" },
                    new Cinema() { Name = "Tilburg", Address = "Heuvelring 14", ZipCode = "5023SZ", City = "Tilburg", Phone = "0135714928", Email = "info@cinematilburg.nl" }
                    );
                context.SaveChanges();

                // Create ratings
                context.Ratings.AddOrUpdate(r => r.Name,
                    new Rating() { Name = "Alle Leeftijden", ImageUrl = "Alle leeftijden.png" },
                    new Rating() { Name = "6 jaar", ImageUrl = "6 jaar.png" },
                    new Rating() { Name = "9 jaar", ImageUrl = "9 jaar.png" },
                    new Rating() { Name = "12 jaar", ImageUrl = "12 jaar.png" },
                    new Rating() { Name = "16 jaar", ImageUrl = "16 jaar.png" },
                    new Rating() { Name = "Angst", ImageUrl = "Angst.png" },
                    new Rating() { Name = "Discriminatie", ImageUrl = "Discriminatie.png" },
                    new Rating() { Name = "Drugs -en Drankmisbruik", ImageUrl = "Drugs -en Drankmisbruik.png" },
                    new Rating() { Name = "Geweld", ImageUrl = "Geweld.png" },
                    new Rating() { Name = "Grof Taalgebruik", ImageUrl = "Grof Taalgebruik.png" },
                    new Rating() { Name = "Seks", ImageUrl = "Seks.png" }
                );
                context.SaveChanges();

                // Create the languages
                context.Languages.AddOrUpdate(l => l.LanguageCode,
                    new Language() { LanguageCode = "NL", LanguageName = "Nederlands" },
                    new Language() { LanguageCode = "EN", LanguageName = "Engels" },
                    new Language() { LanguageCode = "FR", LanguageName = "Frans" }
                );
                context.SaveChanges();

                // Create the Genres
                context.Genres.AddOrUpdate(g => g.Name,
                    new Genre() { Name = "Actie" },
                    new Genre() { Name = "Animatie" },
                    new Genre() { Name = "Avontuur" },
                    new Genre() { Name = "Comedy" },
                    new Genre() { Name = "Drama" },
                    new Genre() { Name = "Historie" },
                    new Genre() { Name = "Kinderfilm" },
                    new Genre() { Name = "Fantasie" },
                    new Genre() { Name = "Romantiek" },
                    new Genre() { Name = "Science-Fiction" },
                    new Genre() { Name = "Thriller" }
                );
                context.SaveChanges();

                //Create the screens
                context.Screens.AddOrUpdate(s => s.ScreenID,
                    new Screen() { ScreenID = 1, ScreenNumber = 1, Size = 120 },
                    new Screen() { ScreenID = 2, ScreenNumber = 2, Size = 120 },
                    new Screen() { ScreenID = 3, ScreenNumber = 3, Size = 120 },
                    new Screen() { ScreenID = 4, ScreenNumber = 4, Size = 60 },
                    new Screen() { ScreenID = 5, ScreenNumber = 5, Size = 50 },
                    new Screen() { ScreenID = 6, ScreenNumber = 6, Size = 50 },
                    new Screen() { ScreenID = 7, ScreenNumber = 1, Size = 190 },
                    new Screen() { ScreenID = 8, ScreenNumber = 2, Size = 190 },
                    new Screen() { ScreenID = 9, ScreenNumber = 3, Size = 190 },
                    new Screen() { ScreenID = 10, ScreenNumber = 4, Size = 60 },
                    new Screen() { ScreenID = 11, ScreenNumber = 5, Size = 60 }
                );
                context.SaveChanges();

                // Create the Join table cinema with screen
                AddOrUpdateScreen(context, "Breda", 1);
                AddOrUpdateScreen(context, "Breda", 2);
                AddOrUpdateScreen(context, "Breda", 3);
                AddOrUpdateScreen(context, "Breda", 4);
                AddOrUpdateScreen(context, "Breda", 5);
                AddOrUpdateScreen(context, "Breda", 6);
                AddOrUpdateScreen(context, "Tilburg", 7);
                AddOrUpdateScreen(context, "Tilburg", 8);
                AddOrUpdateScreen(context, "Tilburg", 9);
                AddOrUpdateScreen(context, "Tilburg", 10);
                AddOrUpdateScreen(context, "Tilburg", 11);
                context.SaveChanges();

                // Create the seats
                Zalen1tot3(context);
                Zaal4(context);
                Zalen5en6(context);
                Zalen7tot9(context);
                Zalen10en11(context);

                // Create the tariffs
                context.Tariffs.AddOrUpdate(t => t.Name,
                    new Tariff() { Name = "Normaal", Price = 8.50, EnglishName = "Normal" },
                    new Tariff() { Name = "Kinderkorting", Price = 7, EnglishName = "Child Discount" },
                    new Tariff() { Name = "Studentenkorting", Price = 7, EnglishName = "Student Discount" },
                    new Tariff() { Name = "65+ Reductie", Price = 7, EnglishName = "Senior Discount" },
                    new Tariff() { Name = "Popcorn Arrangement", Price = 6.50, EnglishName = "Popcorn Arrangement" },
                    new Tariff() { Name = "3D Bril", Price = 1.50, EnglishName = "3D Glasses" }
                );
                context.SaveChanges();

                // Create the movies
                context.Movies.AddOrUpdate(m => m.Title,
                    new Movie() { Title = "American Sniper", Duration = 134, Genres = new List<Genre>(), Is3DAvailable = true, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("20-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "Navy SEAL sniper Chris Kyle's pinpoint accuracy saves countless lives on the battlefield and turns him into a legend. Back home to his wife and kids after four tours of duty, however, Chris finds that it is the war he can't leave behind.", Description = "U.S. Navy SEAL Chris Kyle gaat naar Irak met slechts één missie: zijn wapenbroeders beschermen. Met zijn vaste hand redt hij talloze levens op het slagveld en dankzij de verhalen over zijn heldenmoed krijgt hij al snel de bijnaam 'Legend'. Maar ook achter de vijandelijke linie groeit zijn reputatie. Er wordt een prijs op zijn hoofd gezet en hij wordt het doelwit van rebellen. Ondanks het gevaar, en de tol die het eist van het thuisfront, gaat Chris in totaal vier keer naar Irak en wordt hij het levende symbool van het SEAL-motto: Nooit iemand achterlaten. Maar eenmaal thuis komt hij er al snel achter dat het de oorlog is die hij niet kan achterlaten.", Director = "Clint Eastwood", ImageURL = "American Sniper.jpg", ImdbURL = "http://www.imdb.com/title/tt2179136/", ImdbRating = "7,4", TrailerURL = "http://www.imdb.com/video/imdb/vi3484134937/", BannerURL = "American Sniper.jpg" },
                    new Movie() { Title = "Big Hero 6", Duration = 107, Genres = new List<Genre>(), Is3DAvailable = true, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("20-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "The special bond that develops between plus-sized inflatable robot Baymax, and prodigy Hiro Hamada, who team up with a group of friends to form a band of high-tech heroes.", Description = "Hiro Hamada is een wonderkind dat uitblinkt in robottechnologie. Wanneer hij in een gevaarlijk plot terechtkomt, moet hij zijn beste maatje, de robot Baymax, en zijn verschillende vrienden transformeren tot een groep hightech-helden.", Director = "Don Hall & Chris Williams", ImageURL = "Big Hero 6.jpg", ImdbURL = "http://www.imdb.com/title/tt2245084/", ImdbRating = "8,0", TrailerURL = "http://www.imdb.com/video/imdb/vi513650457/", BannerURL = "Big Hero 6.jpg" },
                    new Movie() { Title = "Chappie", Duration = 120, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("20-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "In the near future, crime is patrolled by a mechanized police force. When one police droid, Chappie, is stolen and given new programming, he becomes the first robot with the ability to think and feel for himself.", Description = "Eén van deze robots wordt gestolen en opnieuw geprogrammeerd waardoor hij de eerste robot wordt die zelf kan nadenken en gevoelens heeft. Wanneer deze androïde, genaamd Chappie, gekidnapt wordt door criminelen vormt hij al snel een bedreiging voor de politiestaat. Alle middelen worden ingezet om hem te vernietigen en te voorkomen dat er meerdere soortgelijke androïden worden geproduceerd.", Director = "Neill Blomkamp", ImageURL = "Chappie.jpg", ImdbURL = "http://www.imdb.com/title/tt1823672/", ImdbRating = "7,4", TrailerURL = "http://www.imdb.com/video/imdb/vi704556569/", BannerURL = "Chappie.jpg" },
                    new Movie() { Title = "Fifty Shades of Grey", Duration = 125, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("20-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "Literature student Anastasia Steele's life changes forever when she meets handsome, yet tormented, billionaire Christian Grey.", Description = "Sinds de eerste uitgave is de Fifty Shades trilogie in 52 verschillende talen vertaald en zijn er wereldwijd meer dan 90 miljoen boeken verkocht. Het is één van de best verkochte boekenreeksen ooit. De iconische rollen van Christian Grey en Anastasia Steele worden vertolkt door Jamie Dornan en Dakota Johnson.", Director = "Sam Taylor-Johnson", ImageURL = "Fifty Shades of Grey.jpg", ImdbURL = "http://www.imdb.com/title/tt2322441/", ImdbRating = "4,2", TrailerURL = "http://www.imdb.com/video/imdb/vi2636688921/", BannerURL = "Fifty Shades of Grey.jpg" },
                    new Movie() { Title = "Focus", Duration = 105, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("20-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "In the midst of veteran con man Nicky's latest scheme, a woman from his past - now an accomplished femme fatale - shows up and throws his plans for a loop.", Description = "Will Smith speelt Nicky, een meester op het gebied van misleiding, die een relatie krijgt met de nieuwbakken oplichtster Jess (Margot Robbie). Terwijl hij haar de fijne kneepjes van het vak leert, ziet hij in haar een risico en verbreekt de relatie. Drie jaar later duikt zijn voormalige vlam, nu een volleerde femme fatale, op in Buenos Aires tijdens een lucratieve autorace. Nicky speelt een levensgevaarlijk spel, zij gooit al zijn plannen in de war en raakt de doortrapte oplichter volledig van de wijs. ", Director = "Glenn Ficarra & John Requa", ImageURL = "Focus.jpg", ImdbURL = "http://www.imdb.com/title/tt2381941/", ImdbRating = "7,0", TrailerURL = "http://www.imdb.com/video/imdb/vi3530599961/", BannerURL = "Focus.jpg" },
                    new Movie() { Title = "Gooische Vrouwen 2", Duration = 106, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("20-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "This movie should not be watched", Description = "Als Claire totaal veranderd terugkomt uit Burkina Faso herkennen haar vriendinnen haar amper. Voor Cheryl is de maat vol als ze ontdekt dat Martin haar voor de zoveelste keer bedriegt: ze zet in op een echtscheiding. Anouk, ook ongelukkig in de liefde, dreigt een eetprobleem te ontwikkelen. Roelien gaat juist in op het huwelijksaanzoek van Evert.", Director = "Will Koopman", ImageURL = "Gooische Vrouwen 2.jpg", ImdbURL = "http://www.imdb.com/title/tt3420108/", ImdbRating = "6,1", TrailerURL = "https://www.youtube.com/embed/u_S3zc9lkdI/", BannerURL = "Gooische Vrouwen 2.jpg" },
                    new Movie() { Title = "Fast & Furious 7", Duration = 138, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("02-05-2015", new CultureInfo("nl-NL")), DescriptionEN = "Deckard Shaw seeks revenge against Dominic Toretto and his family for the death of his brother.", Description = "Vin Diesel, Paul Walker en Dwayne Johnson zijn opnieuw samen te zien in de hoofdrollen van Fast & Furious 7. James Wan regisseert dit nieuwe deel uit de succesvolle filmreeks waarin ook Michelle Rodriguez, Jordana Brewster, Tyrese Gibson, Chris ´Ludacris´ Bridges, Elsa Patakay en Lucas Black terugkeren. De cast wordt versterkt door internationale sterren Jason Statham, Djimon Hounsou, Tony Jaa, Ronda Rousey en Kurt Russell. Neal H. Moritz, Vin Diesel en Michael Fottrel keren terug als producenten van deze film geschreven door Chris Morgan. ", Director = "James Wan", ImageURL = "fastfurious7.jpg", ImdbURL = "http://www.imdb.com/title/tt2820852/", ImdbRating = "Not yet released", TrailerURL = "http://www.imdb.com/video/imdb/vi230273305/", BannerURL = "Fast & Furious 7.jpg" },
                    new Movie() { Title = "Avengers: Age of Ultron", Duration = 160, Genres = new List<Genre>(), Is3DAvailable = true, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("10-06-2015", new CultureInfo("nl-NL")), DescriptionEN = "When Tony Stark tries to jumpstart a dormant peacekeeping program, things go awry and it is up to the Avengers to stop the villainous Ultron from enacting his terrible plans.", Description = "Wanneer een vredesprogramma van Tony Stark verschrikkelijk verkeerd gaat moet het team van superhelden de strijd aangaan met de kwade en alles vernietigende Ultron. Marvel Studios presenteert Avengers: Age of Ultron, het epische vervolg op de grootste superheldenfilm aller tijden. Als Tony Stark probeert om een slapend vredesinitiatief nieuw leven in te blazen, gaat het goed mis en de machtigste helden van de aarde, waaronder Iron Man, Captain America, Thor, The Incredible Hulk, Black Widow en Hawkeye worden tot het uiterste op de proef gesteld als het lot van de aarde in hun handen ligt. Als de kwaadaardige Ultron verschijnt, is het aan The Avengers om zijn verschrikkelijke plannen te stoppen en al snel zorgen ongemakkelijke bondgenootschappen en onverwachte acties voor een uniek episch en wereldwijd avontuur. ", Director = "Josh Wedon", ImageURL = "avengers2.jpg", ImdbURL = "http://www.imdb.com/title/tt2395427/", ImdbRating = "Not yet released", TrailerURL = "http://www.imdb.com/video/imdb/vi2906697241/", BannerURL = "The Avengers Age of Ultron.jpg" },
                    new Movie() { Title = "Run All Night", Duration = 114, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("13-03-2015", new CultureInfo("nl-NL")), DescriptionEN = "Mobster and hit man Jimmy Conlon has one night to figure out where his loyalties lie: with his estranged son, Mike, whose life is in danger, or his longtime best friend, mob boss Shawn Maguire, who wants Mike to pay for the death of his own son.", Description = "De in Brooklyn wonende gangster en huurmoordenaar Jimmy Conlon (Neeson), alias The Gravedigger, heeft betere tijden gekend. Jimmy, de beste vriend van maffiabaas Shawn Maguire (Ed Harris), is nu 55 en wordt achtervolgd door zijn zonden uit het verleden. Conlon wordt al ruim dertig jaar in de gaten gehouden door een vasthoudende rechercheur (D’Onofrio). De laatste jaren zoekt Jimmy voornamelijk zijn heil in een whiskyglas. Maar als zijn zoon Mike (Kinnaman) het doelwit wordt, moet Jimmy een keuze maken tussen de maffiafamilie waar hij ooit voor koos, en zijn echte familie, waar hij al heel lang geen contact meer mee heeft. Mike slaat op de vlucht en Jimmy moet voorkomen dat zijn zoon niet het hetzelfde lot ondergaat als hij, namelijk eindigend aan de verkeerde kant van een vuurwapen. Jimmy heeft slechts één nacht om erachter te komen aan welke wereld hij trouw is, en om alles weer recht te zetten. ", Director = "Jaume Collet-Serra", ImageURL = "Run All Night.jpg", ImdbURL = "http://www.imdb.com/title/tt2199571/", ImdbRating = "7,3", TrailerURL = "http://www.imdb.com/video/imdb/vi4227706393/", BannerURL = "Run All Night.jpg" },
                    new Movie() { Title = "Kingsman: The Secret Service", Duration = 129, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("13-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "A spy organization recruits an unrefined, but promising street kid into the agency's ultra-competitive training program, just as a global threat emerges from a twisted tech genius.", Description = "In de film Kingsman: The Secret Service rekruteert een uiterst geheime organisatie voor spionnen een ongepolijst maar talentvol straatschoffie voor hun trainingsprogramma. Precies op het moment dat de wereld wordt bedreigd door een IT-genie met kwaadaardige bedoelingen. ", Director = "Matthew Vaughn", ImageURL = "Kingsman.jpg", ImdbURL = "http://www.imdb.com/title/tt2802144/", ImdbRating = "8,2", TrailerURL = "http://www.imdb.com/video/imdb/vi1901244185/", BannerURL = "Kingsman.jpg" },
                    new Movie() { Title = "Birdman", Duration = 119, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("14-11-2014", new CultureInfo("nl-NL")), DescriptionEN = "A washed-up actor, who once played an iconic superhero, battles his ego and attempts to recover his family, his career and himself in the days leading up to the opening of his Broadway play.", Description = "Birdman vertelt het verhaal van acteur Riggan Thomson (Michael Keaton). Ooit wereldberoemd door zijn rol als een iconische superheld, nu worstelt hij met een toneelstuk op Broadway. In de aanloop naar de première gaat hij de strijd aan met zijn ego en probeert hij zijn familie, zij carrière en zichzelf weer op de rails te krijgen. ", Director = "Alejandro González Iñárritu", ImageURL = "Birdman.jpg", ImdbURL = "http://www.imdb.com/title/tt2562232/", ImdbRating = "8,0", TrailerURL = "http://www.imdb.com/video/imdb/vi1378069529/", BannerURL = "Birdman.jpg" },
                    new Movie() { Title = "Paddington", Duration = 95, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("16-01-2015", new CultureInfo("nl-NL")), DescriptionEN = "A young Peruvian bear travels to London in search of a home. Finding himself lost and alone at Paddington Station, he meets the kindly Brown family, who offer him a temporary haven.", Description = "Een jonge Peruviaanse beer met een grote passie voor alles dat Brits is reist naar Londen om een huisje te vinden. Alleen en verdwaald eindigt het beertje op Paddington Station, waar hij er achter komt dat het stadsleven niks voor hem is. Op het station wordt hij gevonden door de familie Brown, die het beertje in huis neemt. Alles lijkt goed te gaan voor het beertje totdat een museumconservator het unieke beertje ontdekt. Een live-action animatie gebaseerd op de populaire kinderboeken van Michael Bond.", Director = "Paul King", ImageURL = "Paddington.jpg", ImdbURL = "http://www.imdb.com/title/tt1109624/", ImdbRating = "7,5", TrailerURL = "http://www.imdb.com/video/imdb/vi706391833/", BannerURL = "Paddington.jpg" },
                    new Movie() { Title = "Michiel de Ruyter", Duration = 151, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("28-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "When the young republic of The Netherlands is attacked by England, France and Germany and the country itself is on the brink of civil war, only one man can lead the county's strongest weapon, the Dutch fleet: Michiel de Ruyter.", Description = "Intriges, samenzweringen, spectaculaire zeeslagen tegen de Engelsen met De Ruyter als aanvoerder van de Nederlandse vloot en de dubieuze rol van de Oranjes. Gekeurd voor 16 jaar en ouder; geen toegang indien jonger. Bij twijfel wordt legitimatie gevraagd. Halverwege de 17e eeuw is Nederland een van de grootste zeevarende naties ter wereld. Na het overlijden van de legendarische admiraal Maarten Tromp, is Michiel de Ruyter zijn gedoodverfde opvolger. In diverse zeeslagen heeft De Ruyter getoond dat hij een briljant strateeg is. Politiek gezien is het Hollandse volk verdeeld. Oranjeaanhangers en republikeinen vechten om de macht. De hyperintelligente republikein Johan de Witt is als raadspensionaris de ... machtigste man van de Republiek der Nederlanden, maar zijn tegenstanders proberen de onzekere, maar zeer ambitieuze jonge prins Willem III stadhouder te maken. De republikeinen is er alles aan gelegen dat de oorlog met de Engelsen wordt gewonnen. Met een ingenieuze strategie weet De Ruyter op spectaculaire wijze de Engelse vloot te verpletteren. Bij het volk is hij ongekend populair, maar onderlinge complotten en samenzweringen leiden tot conflicten tussen de regerende macht en de aanhangers van de prins. Het Huis van Oranje scherpt de messen en De Ruyter wordt meegesleurd in een stroom van politieke ontwikkelingen. De Ruyter en zijn gezin dreigen het slachtoffer te worden van een politiek steekspel waarin hij op een onmogelijke missie wordt gestuurd. ", Director = "Roel Reiné", ImageURL = "Michiel de Ruyter.jpg", ImdbURL = "http://www.imdb.com/title/tt2544766/", ImdbRating = "7,9", TrailerURL = "https://www.youtube.com/embed/j21t1i_SiKI", BannerURL = "Michiel de Ruyter.jpg" },
                    new Movie() { Title = "Seventh Son", Duration = 102, Genres = new List<Genre>(), Is3DAvailable = true, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("20-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "Young Thomas is apprenticed to the local Spook to learn to fight evil spirits. His first great challenge comes when the powerful Mother Malkin escapes her confinement while the Spook is away.", Description = "Lang geleden werd de wereld bedreigd door een kwaad dat een oorlog wilde ontketenen tussen bovennatuurlijke krachten en de mensheid. Meester Gregory (Jeff Bridges) is een ridder die de boosaardige en machtige heks Moeder Malkin (Julianne Moore) opsloot. Maar nu is ze ontsnapt en neemt ze wraak. Moeder Malkin roept al haar volgelingen op en bereidt zich voor om haar toorn op de nietsvermoedende wereld los te laten. Er is slechts één belemmering: Meester Gregory. Gregory komt oog in oog te staan met het dodelijke kwaad waarvan hij altijd al vreesde dat het zou terugkeren.  Hij heeft de tijd tot de volgende volle maan om iets te doen wat normaal gesproken jaren duurt: het opleiden van zijn nieuwe leerling Tom Ward (Ben Barnes). Het lot van de mensheid is in handen van de zevende zoon van een zevende zoon.", Director = "Sergei Bodrov", ImageURL = "Seventh Son.jpg", ImdbURL = "http://www.imdb.com/title/tt1121096/", ImdbRating = "5,7", TrailerURL = "http://www.imdb.com/video/imdb/vi1803464473/", BannerURL = "Seventh Son.jpg" },
                    new Movie() { Title = "Divergent Series: Insurgent", Duration = 123, Genres = new List<Genre>(), Is3DAvailable = true, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("06-02-2015", new CultureInfo("nl-NL")), DescriptionEN = "Beatrice Prior must confront her inner demons and continue her fight against a powerful alliance which threatens to tear her society apart with the help from others on her side.", Description = "In het tweede deel van The Divergent Series: Insurgent krijgt Tris het steeds moeilijker, ze zoekt voor bondgenoten en antwoorden in hetgeen wat is overgebleven van het futuristische Chicago. Tris en Four zijn voortvluchtigen en worden achternagezeten door Jeanine, de leider van de factie Eruditie. In een race tegen de klok moeten ze uitzoeken waar de familie van Tris zijn leven voor heeft gegeven en waarom de leiders van Eruditie er alles aan doen om ze tegen te houden. Achtervolgd door hun keuzes in het verleden, zijn zij wanhopig om hun geliefden te beschermen en worden Tris en Four met de ene na de andere onmogelijke uitdaging geconfronteerd. Langzaam maar zeker ontrafelen zij de waarheid over hun verleden en uiteindelijk over de toekomst van hun wereld. ", Director = "Robert Schwentke", ImageURL = "Insurgent.jpg", ImdbURL = "http://www.imdb.com/title/tt2908446/", ImdbRating = "7,6", TrailerURL = "http://www.imdb.com/video/imdb/vi4100435481/", BannerURL = "Insurgent.jpg" },
                    new Movie() { Title = "The Imitation Game", Duration = 114, Genres = new List<Genre>(), Is3DAvailable = false, Ratings = new List<Rating>(), Languages = new List<Language>(), ReleaseDate = Convert.ToDateTime("25-12-2014", new CultureInfo("nl-NL")), DescriptionEN = "During World War II, mathematician Alan Turing tries to crack the enigma code with help from fellow mathematicians.", Description = "Dit indrukwekkende drama, genomineerd voor acht Oscars waaronder Beste film, regie en acteur, gaat over het leven en werk van wetenschapper Alan Turing (Benedict Cumberbatch), wiens uitvinding essentieel was voor de beëindiging van de 2e Wereldoorlog. De briljante maar emotioneel gecompliceerde Brit hielp met zijn werk de oorlog te verkorten en heeft daarmee de levens van tienduizenden mensen gered. In de winter van 1952 werd hij door de autoriteiten opgepakt voor het toen strafbare feit homoseksualiteit, met een verschrikkelijke veroordeling tot gevolg. Wat echter bij niemand bekend was, was dat Turing enkele jaren daarvoor tijdens de oorlog in het diepste geheim leiding gaf aan een groep van knappe koppen. Hun opdracht was het kraken van de Enigma code die de Duitsers voor al hun communicatie gebruikten. De machine die Turing uitvond was ... niet alleen essentieel tijdens de oorlog, ook was dit het prototype voor onze hedendaagse computer. Met deze film krijgt één van de meest onbekende, maar immens belangrijke figuren uit de wereldgeschiedenis eindelijk de erkenning die hij verdient. The Imitation Game, met Benedict Cumberbatch, Keira Knightley, Mark Strong en Matthew Goode in de hoofdrol, werd zeer positief ontvangen door pers en publiek op diverse prestigieuze filmfestivals, en won daar ook meerdere malen de publieksprijs. ", Director = "Morten Tyldum", ImageURL = "Imitation Game.jpg", ImdbURL = "http://www.imdb.com/title/tt2084970/", ImdbRating = "8,2", TrailerURL = "http://www.imdb.com/video/imdb/vi3398414105/", BannerURL = "Imitation Game.jpg" }

                );
                context.SaveChanges();

                // Create join table Movie met Cinema
                AddOrUpdateMovie(context, "Breda", "American Sniper");
                AddOrUpdateMovie(context, "Breda", "Big Hero 6");
                AddOrUpdateMovie(context, "Breda", "Chappie");
                AddOrUpdateMovie(context, "Breda", "Fifty Shades of Grey");
                AddOrUpdateMovie(context, "Breda", "Focus");
                AddOrUpdateMovie(context, "Breda", "Fast & Furious 7");
                AddOrUpdateMovie(context, "Breda", "Avengers: Age of Ultron");
                AddOrUpdateMovie(context, "Breda", "Run All Night");
                AddOrUpdateMovie(context, "Breda", "Kingsman: The Secret Service");
                AddOrUpdateMovie(context, "Breda", "Birdman");
                AddOrUpdateMovie(context, "Breda", "Paddington");
                AddOrUpdateMovie(context, "Breda", "Seventh Son");
                AddOrUpdateMovie(context, "Breda", "Divergent Series: Insurgent");
                AddOrUpdateMovie(context, "Breda", "The Imitation Game");
                AddOrUpdateMovie(context, "Tilburg", "American Sniper");
                AddOrUpdateMovie(context, "Tilburg", "Big Hero 6");
                AddOrUpdateMovie(context, "Tilburg", "Chappie");
                AddOrUpdateMovie(context, "Tilburg", "Michiel de Ruyter");
                AddOrUpdateMovie(context, "Tilburg", "Focus");
                AddOrUpdateMovie(context, "Tilburg", "Fast & Furious 7");
                AddOrUpdateMovie(context, "Tilburg", "Avengers: Age of Ultron");
                AddOrUpdateMovie(context, "Tilburg", "Run All Night");
                AddOrUpdateMovie(context, "Tilburg", "Kingsman: The Secret Service");
                AddOrUpdateMovie(context, "Tilburg", "Gooische Vrouwen 2");
                AddOrUpdateMovie(context, "Tilburg", "Paddington");
                AddOrUpdateMovie(context, "Tilburg", "Seventh Son");
                AddOrUpdateMovie(context, "Tilburg", "Divergent Series: Insurgent");
                AddOrUpdateMovie(context, "Tilburg", "The Imitation Game");
                context.SaveChanges();

                // Create the genre, language and rating join table
                AddOrUpdateGenre(context, "American Sniper", "Actie");
                AddOrUpdateLanguage(context, "American Sniper", "Engels");
                AddOrUpdateRating(context, "American Sniper", "16 jaar");
                AddOrUpdateRating(context, "American Sniper", "Grof Taalgebruik");
                AddOrUpdateRating(context, "American Sniper", "Geweld");
                AddOrUpdateGenre(context, "Big Hero 6", "Animatie");
                AddOrUpdateGenre(context, "Big Hero 6", "Kinderfilm");
                AddOrUpdateLanguage(context, "Big Hero 6", "Engels");
                AddOrUpdateLanguage(context, "Big Hero 6", "Nederlands");
                AddOrUpdateRating(context, "Big Hero 6", "6 jaar");
                AddOrUpdateRating(context, "Big Hero 6", "Angst");
                AddOrUpdateGenre(context, "Chappie", "Actie");
                AddOrUpdateGenre(context, "Chappie", "Science-Fiction");
                AddOrUpdateLanguage(context, "Chappie", "Engels");
                AddOrUpdateRating(context, "Chappie", "16 jaar");
                AddOrUpdateRating(context, "Chappie", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Chappie", "Geweld");
                AddOrUpdateGenre(context, "Fifty Shades of Grey", "Romantiek");
                AddOrUpdateGenre(context, "Fifty Shades of Grey", "Drama");
                AddOrUpdateLanguage(context, "Fifty Shades of Grey", "Engels");
                AddOrUpdateRating(context, "Fifty Shades of Grey", "16 jaar");
                AddOrUpdateRating(context, "Fifty Shades of Grey", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Fifty Shades of Grey", "Seks");
                AddOrUpdateGenre(context, "Focus", "Actie");
                AddOrUpdateGenre(context, "Focus", "Comedy");
                AddOrUpdateLanguage(context, "Focus", "Engels");
                AddOrUpdateRating(context, "Focus", "12 jaar");
                AddOrUpdateRating(context, "Focus", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Focus", "Geweld");
                AddOrUpdateGenre(context, "Gooische Vrouwen 2", "Comedy");
                AddOrUpdateLanguage(context, "Gooische Vrouwen 2", "Nederlands");
                AddOrUpdateRating(context, "Gooische Vrouwen 2", "6 jaar");
                AddOrUpdateRating(context, "Gooische Vrouwen 2", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Gooische Vrouwen 2", "Geweld");
                AddOrUpdateRating(context, "Gooische Vrouwen 2", "Angst");
                AddOrUpdateGenre(context, "Fast & Furious 7", "Actie");
                AddOrUpdateLanguage(context, "Fast & Furious 7", "Engels");
                AddOrUpdateGenre(context, "Avengers: Age of Ultron", "Actie");
                AddOrUpdateGenre(context, "Avengers: Age of Ultron", "Fantasie");
                AddOrUpdateGenre(context, "Avengers: Age of Ultron", "Science-Fiction");
                AddOrUpdateLanguage(context, "Avengers: Age of Ultron", "Engels");
                AddOrUpdateGenre(context, "Run All Night", "Actie");
                AddOrUpdateGenre(context, "Run All Night", "Thriller");
                AddOrUpdateLanguage(context, "Run All Night", "Engels");
                AddOrUpdateRating(context, "Run All Night", "16 jaar");
                AddOrUpdateRating(context, "Run All Night", "Geweld");
                AddOrUpdateRating(context, "Run All Night", "Grof Taalgebruik");
                AddOrUpdateGenre(context, "Kingsman: The Secret Service", "Actie");
                AddOrUpdateGenre(context, "Kingsman: The Secret Service", "Thriller");
                AddOrUpdateLanguage(context, "Kingsman: The Secret Service", "Engels");
                AddOrUpdateRating(context, "Kingsman: The Secret Service", "16 jaar");
                AddOrUpdateRating(context, "Kingsman: The Secret Service", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Kingsman: The Secret Service", "Geweld"); AddOrUpdateGenre(context, "Birdman", "Drama");
                AddOrUpdateLanguage(context, "Birdman", "Engels");
                AddOrUpdateRating(context, "Birdman", "12 jaar");
                AddOrUpdateRating(context, "Birdman", "Seks");
                AddOrUpdateRating(context, "Birdman", "Drugs -en Drankmisbruik");
                AddOrUpdateRating(context, "Birdman", "Discriminatie");
                AddOrUpdateRating(context, "Birdman", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Birdman", "Geweld");
                AddOrUpdateGenre(context, "Paddington", "Comedy");
                AddOrUpdateGenre(context, "Paddington", "Kinderfilm");
                AddOrUpdateLanguage(context, "Paddington", "Nederlands");
                AddOrUpdateLanguage(context, "Paddington", "Engels");
                AddOrUpdateRating(context, "Paddington", "6 jaar");
                AddOrUpdateRating(context, "Paddington", "Angst");
                AddOrUpdateGenre(context, "Michiel de Ruyter", "Avontuur");
                AddOrUpdateGenre(context, "Michiel de Ruyter", "Historie");
                AddOrUpdateLanguage(context, "Michiel de Ruyter", "Nederlands");
                AddOrUpdateRating(context, "Michiel de Ruyter", "Angst");
                AddOrUpdateRating(context, "Michiel de Ruyter", "16 jaar");
                AddOrUpdateRating(context, "Michiel de Ruyter", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Michiel de Ruyter", "Geweld");
                AddOrUpdateGenre(context, "Seventh Son", "Fantasie");
                AddOrUpdateGenre(context, "Seventh Son", "Science-Fiction");
                AddOrUpdateLanguage(context, "Seventh Son", "Engels");
                AddOrUpdateRating(context, "Seventh Son", "12 jaar");
                AddOrUpdateRating(context, "Seventh Son", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Seventh Son", "Geweld");
                AddOrUpdateGenre(context, "Divergent Series: Insurgent", "Actie");
                AddOrUpdateGenre(context, "Divergent Series: Insurgent", "Avontuur");
                AddOrUpdateGenre(context, "Divergent Series: Insurgent", "Science-Fiction");
                AddOrUpdateLanguage(context, "Divergent Series: Insurgent", "Engels");
                AddOrUpdateRating(context, "Divergent Series: Insurgent", "12 jaar");
                AddOrUpdateRating(context, "Divergent Series: Insurgent", "Angst");
                AddOrUpdateRating(context, "Divergent Series: Insurgent", "Grof Taalgebruik");
                AddOrUpdateRating(context, "Divergent Series: Insurgent", "Geweld");
                AddOrUpdateGenre(context, "The Imitation Game", "Drama");
                AddOrUpdateGenre(context, "The Imitation Game", "Thriller");
                AddOrUpdateLanguage(context, "The Imitation Game", "Engels");
                AddOrUpdateRating(context, "The Imitation Game", "12 jaar");
                AddOrUpdateRating(context, "The Imitation Game", "Grof Taalgebruik");
                AddOrUpdateRating(context, "The Imitation Game", "Geweld");
                context.SaveChanges();

                //create the shows
                context.Shows.AddOrUpdate(s => s.ShowID,
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////// Breda shows vandaag/////////////////////////////////////////////////////////////
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 1,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(10)

                    },
                    new Show() {
                        ShowID = 2,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 3,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 4,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Engels",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 5,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 6,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 7,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 8,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 9,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 10,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 11,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 12,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 13,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 14,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 15,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 16,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 17,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 18,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 19,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 20,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 21,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 22,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 23,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 24,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 25,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 26,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 27,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 28,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 29,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 30,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 31,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 32,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 33,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 34,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 35,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 36,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////// +1 dag //////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 37,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 38,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 39,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 40,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 41,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 42,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 43,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 44,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 45,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 46,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 47,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 48,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 49,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 50,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 51,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 52,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 53,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 54,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 55,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 56,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 57,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 58,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 59,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 60,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 61,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 62,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 63,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 64,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 65,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 66,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 67,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 68,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 69,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 70,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 71,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 72,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////////+2 dagen //////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 73,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 74,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 75,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 76,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 77,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 78,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 79,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 80,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 81,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 82,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 83,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 84,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 85,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 86,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 87,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 88,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 89,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 90,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 91,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 92,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 93,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 94,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 95,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 96,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 97,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 98,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 99,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 100,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 101,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 102,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 103,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 104,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 105,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 106,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 107,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 108,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 320,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 321,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 322,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 323,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 324,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 325,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 326,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 327,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 328,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 329,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 330,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 331,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 332,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 333,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 334,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 335,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 336,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 337,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 338,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 339,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 340,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 341,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 342,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 343,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 344,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 345,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 346,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 347,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 348,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 349,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 349,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 350,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 351,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 352,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 353,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 354,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 355,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 356,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 357,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 358,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 359,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 360,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 361,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 362,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 363,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 364,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 365,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 366,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 367,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 368,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 369,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 370,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 371,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 372,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 373,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 374,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 375,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 376,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 377,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 378,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 379,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 380,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 381,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 382,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 383,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 384,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 385,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 386,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 387,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 388,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 389,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 390,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 391,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 392,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 393,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 394,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 395,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 396,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 397,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 398,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 399,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 400,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 401,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 402,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 403,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 404,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 405,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 406,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 407,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 408,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 409,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 410,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 411,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 412,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 413,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 414,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 415,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 416,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 417,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 418,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 419,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 420,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 421,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 422,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 423,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 424,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 425,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 426,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                                        new Show() {
                                            ShowID = 427,
                                            MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                                            ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                                            CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                                            LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                                            Subtitles = null,
                                            Is3D = true,
                                            DateTime = DateTime.Today.AddDays(6).AddHours(10)
                                        },
                    new Show() {
                        ShowID = 428,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 429,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 430,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 431,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 432,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 1).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 433,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 434,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 435,
                        MovieID = context.Movies.Single(m => m.Title == "Fifty Shades of Grey").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 436,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 437,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 438,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 2).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 439,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 440,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 441,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 442,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 443,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 444,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 3).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 445,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 446,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 447,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 448,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 449,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 450,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 4).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 451,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 452,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 453,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 454,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 455,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 456,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 5).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 457,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 458,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 459,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 460,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 461,
                        MovieID = context.Movies.Single(m => m.Title == "Birdman").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 462,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 6).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Breda").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////Tilburg Shows///////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 109,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(10)

                    },
                    new Show() {
                        ShowID = 110,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 111,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 112,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Engels",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 113,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 114,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 115,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 116,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 117,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 118,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 119,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 120,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 121,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 122,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 123,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 124,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 125,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 126,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 127,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 128,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 129,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 130,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 131,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 132,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 133,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(10)
                    },
                    new Show() {
                        ShowID = 134,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 135,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(15)
                    },
                    new Show() {
                        ShowID = 136,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 137,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(20)
                    },
                    new Show() {
                        ShowID = 138,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(22).AddMinutes(30)
                    },

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////// +1 dag /////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 139,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)

                    },
                    new Show() {
                        ShowID = 140,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 142,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 143,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Engels",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 144,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 145,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 146,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 147,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 148,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 149,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 150,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 151,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 152,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 153,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 154,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 155,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 156,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 157,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 158,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 159,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 160,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 161,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 162,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 163,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 164,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(10)
                    },
                    new Show() {
                        ShowID = 165,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 166,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(1).AddHours(15)
                    },
                    new Show() {
                        ShowID = 167,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 168,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(20)
                    },
                    new Show() {
                        ShowID = 169,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(1).AddHours(22).AddMinutes(30)
                    },

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////+ 2 ////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 170,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)

                    },
                    new Show() {
                        ShowID = 171,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 172,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 173,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Engels",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 174,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 175,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 176,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 177,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 178,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 179,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 180,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 181,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 182,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 183,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 184,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 185,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 186,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 187,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 188,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 189,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 190,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 191,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 192,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 193,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 194,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(10)
                    },
                    new Show() {
                        ShowID = 195,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(2).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 196,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(15)
                    },
                    new Show() {
                        ShowID = 197,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 198,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(20)
                    },
                    new Show() {
                        ShowID = 199,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(2).AddHours(22).AddMinutes(30)
                    },

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////// +3 dag ///////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 200,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)

                    },
                    new Show() {
                        ShowID = 201,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 202,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 203,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Engels",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 204,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 205,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 206,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 207,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 208,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 209,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 210,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 211,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 212,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 213,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 214,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 215,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 216,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 217,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 218,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 219,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 220,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 221,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 222,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 223,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 224,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(10)
                    },
                    new Show() {
                        ShowID = 225,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 226,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(3).AddHours(15)
                    },
                    new Show() {
                        ShowID = 227,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 228,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(20)
                    },
                    new Show() {
                        ShowID = 229,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(3).AddHours(22).AddMinutes(30)
                    },

                    //////////////////////////////////////////////////////////////////////Dag4////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 230,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)

                    },
                    new Show() {
                        ShowID = 231,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 232,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 233,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Engels",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 234,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 235,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 236,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 237,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 238,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 239,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 240,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 241,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 242,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 243,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 244,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 245,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 246,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 247,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 248,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 249,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 250,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 251,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 252,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 253,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 254,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(10)
                    },
                    new Show() {
                        ShowID = 255,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(4).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 256,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(15)
                    },
                    new Show() {
                        ShowID = 257,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 258,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(20)
                    },
                    new Show() {
                        ShowID = 259,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(4).AddHours(22).AddMinutes(30)
                    },

                    ////////////////////////////////////////////////////////////Dag5////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 260,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)

                    },
                    new Show() {
                        ShowID = 261,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 262,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 263,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Engels",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 264,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 265,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 266,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 267,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 268,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 269,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 270,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 271,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 272,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 273,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 274,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 275,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 276,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 277,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 278,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 279,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 280,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 281,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 282,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 283,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 284,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(10)
                    },
                    new Show() {
                        ShowID = 285,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 286,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(5).AddHours(15)
                    },
                    new Show() {
                        ShowID = 287,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 288,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(20)
                    },
                    new Show() {
                        ShowID = 289,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(5).AddHours(22).AddMinutes(30)
                    },

                    /////////////////////////////////////////////////////////////////////Dag6/////////////////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    new Show() {
                        ShowID = 290,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)

                    },
                    new Show() {
                        ShowID = 291,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 292,
                        MovieID = context.Movies.Single(m => m.Title == "Big Hero 6").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 293,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Engels",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 294,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 295,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 7).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 296,
                        MovieID = context.Movies.Single(m => m.Title == "American Sniper").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 297,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 298,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 299,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 300,
                        MovieID = context.Movies.Single(m => m.Title == "Gooische Vrouwen 2").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 301,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 8).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 302,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 303,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 304,
                        MovieID = context.Movies.Single(m => m.Title == "Paddington").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 305,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 306,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 307,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 9).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 308,
                        MovieID = context.Movies.Single(m => m.Title == "Chappie").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 309,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 310,
                        MovieID = context.Movies.Single(m => m.Title == "Seventh Son").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 311,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 312,
                        MovieID = context.Movies.Single(m => m.Title == "Divergent Series: Insurgent").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 313,
                        MovieID = context.Movies.Single(m => m.Title == "Focus").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 10).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    },
                    new Show() {
                        ShowID = 314,
                        MovieID = context.Movies.Single(m => m.Title == "Michiel de Ruyter").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Nederlands").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(10)
                    },
                    new Show() {
                        ShowID = 315,
                        MovieID = context.Movies.Single(m => m.Title == "Kingsman: The Secret Service").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = true,
                        DateTime = DateTime.Today.AddDays(6).AddHours(12).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 316,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(15)
                    },
                    new Show() {
                        ShowID = 317,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(17).AddMinutes(15)
                    },
                    new Show() {
                        ShowID = 318,
                        MovieID = context.Movies.Single(m => m.Title == "The Imitation Game").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = null,
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(20)
                    },
                    new Show() {
                        ShowID = 319,
                        MovieID = context.Movies.Single(m => m.Title == "Run All Night").MovieID,
                        ScreenID = context.Screens.Single(s => s.ScreenID == 11).ScreenID,
                        CinemaID = context.Cinemas.Single(c => c.Name == "Tilburg").CinemaID,
                        LanguageID = context.Languages.Single(l => l.LanguageName == "Engels").LanguageID,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddDays(6).AddHours(22).AddMinutes(30)
                    }
                );
                context.SaveChanges();

                context.Tariffs.AddOrUpdate(t => t.Name,
                    new Tariff() { Name = "Normaal", Price = 8.50, EnglishName = "Normal" },
                    new Tariff() { Name = "Kinderkorting", Price = 7, EnglishName = "Child Discount" },
                    new Tariff() { Name = "Studentenkorting", Price = 7, EnglishName = "Student Discount" },
                    new Tariff() { Name = "65+ Reductie", Price = 7, EnglishName = "Senior Discount" },
                    new Tariff() { Name = "Popcorn Arrangement", Price = 6.50, EnglishName = "Popcorn Arrangement" },
                    new Tariff() { Name = "3D Bril", Price = 1.50, EnglishName = "3D Glasses" }
                );
                context.SaveChanges();


            } catch (DbEntityValidationException ex) {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors) {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors) {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }

        void AddOrUpdateGenre(EFDbContext context, string movieTitle, string genreName) {
            var movie = context.Movies.SingleOrDefault(m => m.Title == movieTitle);
            var genre = movie.Genres.SingleOrDefault(g => g.Name == genreName);
            if (genre == null)
                movie.Genres.Add(context.Genres.Single(g => g.Name == genreName));
        }

        void AddOrUpdateLanguage(EFDbContext context, string movieTitle, string languageName) {
            var movie = context.Movies.SingleOrDefault(m => m.Title == movieTitle);
            var language = movie.Languages.SingleOrDefault(l => l.LanguageName == languageName);
            if (language == null)
                movie.Languages.Add(context.Languages.Single(l => l.LanguageName == languageName));
        }

        void AddOrUpdateRating(EFDbContext context, string movieTitle, string ratingName) {
            var movie = context.Movies.SingleOrDefault(m => m.Title == movieTitle);
            var rating = movie.Ratings.SingleOrDefault(r => r.Name == ratingName);
            if (rating == null)
                movie.Ratings.Add(context.Ratings.Single(r => r.Name == ratingName));
        }

        void AddOrUpdateMovie(EFDbContext context, string CinemaName, string MovieTitle) {
            var cinema = context.Cinemas.SingleOrDefault(m => m.Name == CinemaName);
            var movie = cinema.Movies.SingleOrDefault(r => r.Title == MovieTitle);
            if (movie == null)
                cinema.Movies.Add(context.Movies.Single(r => r.Title == MovieTitle));
        }

        void AddOrUpdateScreen(EFDbContext context, string CinemaName, int ScreenID) {
            var cinema = context.Cinemas.SingleOrDefault(m => m.Name == CinemaName);
            var screen = cinema.Screens.SingleOrDefault(r => r.ScreenID == ScreenID);
            if (screen == null)
                cinema.Screens.Add(context.Screens.Single(r => r.ScreenID == ScreenID));
        }

        public void Zalen1tot3(EFDbContext context) {
            //Aantal zalen
            int hall = 3;
            //Aantal rijen
            int rijen = 8;
            //Aantal stoelen op een rij
            int stoelen = 15;
            //Loop door zalen
            for (int i = 1; i < hall + 1; i++) {

                //Loop door rijen
                for (int j = 1; j < rijen + 1; j++) {

                    //loop door stoelen
                    for (int k = 1; k < stoelen + 1; k++) {
                        context.Seats.AddOrUpdate(s => s.SeatID, new Seat { SeatID = IDForSeed, ScreenID = i, RowNumber = j, SeatNumber = k, Vacated = false });
                        IDForSeed++;
                    }
                }
            }
            context.SaveChanges();
        }

        public void Zaal4(EFDbContext context) {
            //Aantal zalen
            int hall = 4;
            //Aantal rijen
            int rijen = 6;
            //Aantal stoelen op een rij
            int stoelen = 10;
            //loop door zalen
            for (int i = 4; i < hall + 1; i++) {
                //loop door rijen
                for (int j = 1; j < rijen + 1; j++) {
                    //loop door stoelen
                    for (int k = 1; k < stoelen + 1; k++) {
                        context.Seats.AddOrUpdate(s => s.SeatID, new Seat { SeatID = IDForSeed, ScreenID = i, RowNumber = j, SeatNumber = k, Vacated = false });
                        IDForSeed++;
                    }
                }
            }
            context.SaveChanges();
        }

        public void Zalen5en6(EFDbContext context) {
            //Aantal zalen
            int hall = 6;
            //Aantal rijen
            int rijen = 4;
            //Aantal stoelen op een rij
            int stoelen = 10;
            int stoelen2 = 15;
            //loop door hallen
            for (int i = 5; i < hall + 1; i++) {
                //loop door rijen
                for (int j = 1; j < rijen + 1; j++) {
                    //loop door de stoelen
                    if (j < 3) {
                        for (int k = 1; k < stoelen + 1; k++) {
                            context.Seats.AddOrUpdate(s => s.SeatID, new Seat { SeatID = IDForSeed, ScreenID = i, RowNumber = j, SeatNumber = k, Vacated = false });
                            IDForSeed++;
                        }
                    } else {
                        for (int k = 1; k < stoelen2 + 1; k++) {
                            context.Seats.AddOrUpdate(s => s.SeatID, new Seat { SeatID = IDForSeed, ScreenID = i, RowNumber = j, SeatNumber = k, Vacated = false });
                            IDForSeed++;
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        public void Zalen7tot9(EFDbContext context) {
            //Aantal zalen
            int hall = 9;
            //Aantal rijen
            int rijen = 10;
            //Aantal stoelen op een rij
            int stoelen = 19;
            //Loop door zalen
            for (int i = 7; i < hall + 1; i++) {

                //Loop door rijen
                for (int j = 1; j < rijen + 1; j++) {

                    //loop door stoelen
                    for (int k = 1; k < stoelen + 1; k++) {
                        context.Seats.AddOrUpdate(s => s.SeatID, new Seat { SeatID = IDForSeed, ScreenID = i, RowNumber = j, SeatNumber = k, Vacated = false });
                        IDForSeed++;
                    }
                }
            }
            context.SaveChanges();
        }

        public void Zalen10en11(EFDbContext context) {
            //Aantal zalen
            int hall = 11;
            //Aantal rijen
            int rijen = 5;
            //Aantal stoelen op een rij
            int stoelen = 10;
            int stoelen2 = 15;
            //loop door hallen
            for (int i = 10; i < hall + 1; i++) {
                //loop door rijen
                for (int j = 1; j < rijen + 1; j++) {
                    //loop door de stoelen
                    if (j < 3) {
                        for (int k = 1; k < stoelen + 1; k++) {
                            context.Seats.AddOrUpdate(s => s.SeatID, new Seat { SeatID = IDForSeed, ScreenID = i, RowNumber = j, SeatNumber = k, Vacated = false });
                            IDForSeed++;
                        }
                    } else {
                        for (int k = 1; k < stoelen2 + 1; k++) {
                            context.Seats.AddOrUpdate(s => s.SeatID, new Seat { SeatID = IDForSeed, ScreenID = i, RowNumber = j, SeatNumber = k, Vacated = false });
                            IDForSeed++;
                        }
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
