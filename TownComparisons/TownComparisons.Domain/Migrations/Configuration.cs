using TownComparisons.Domain.Entities;
using TownComparisons.Domain.WebServices;

namespace TownComparisons.Domain.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TownComparisons.Domain.DAL.TownComparisonsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TownComparisons.Domain.DAL.TownComparisonsContext context)
        {

            string koladaWebServiceName = new KoladaTownWebService().GetName();



            #region CategoryStuff

            //first delete any previous categories (including it's associations to property queries and organisational units)
            foreach (var groupCategory in context.GroupCategories)
            {
                context.GroupCategories.Remove(groupCategory);
            }

            //GRUNDSKOLA
            PropertyQueryInfo query1 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N15030",
                OriginalTitle = "Lärare med pedagogisk högskoleexamen i grundskola, lägeskommun, (%)",
                Title = "Lärare med pedagogisk högskoleexamen i grundskola, lägeskommun, (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };
            PropertyQueryInfo query2 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N15033",
                OriginalTitle = "Elever/lärare (årsarbetare) i grundskola, lägeskommun, antal",
                Title = "Elever/lärare (årsarbetare) i grundskola, lägeskommun, antal",
                Type = PropertyQuery.TYPE_STANDARD
            };
            PropertyQueryInfo query3 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N15406",
                OriginalTitle = "Elever i åk. 9 som minst uppnått kunskapskraven för Godkänd i ämnesprovet i matematik, kommunala skolor, andel (%)",
                Title = "Elever i åk. 9 som minst uppnått kunskapskraven för Godkänd i ämnesprovet i matematik, kommunala skolor, andel (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };
            PropertyQueryInfo query4 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N15807",
                OriginalTitle = "Elever i grundskola belägen i kommunen, antal",
                Title = "Elever i grundskola belägen i kommunen, antal",
                Type = PropertyQuery.TYPE_STANDARD
            };
            PropertyQueryInfo query5 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N15403",
                OriginalTitle = "Elever i åk. 9, meritvärde kommunala skolor, genomsnitt (16 ämnen)",
                Title = "Elever i åk. 9, meritvärde kommunala skolor, genomsnitt (16 ämnen)",
                Type = PropertyQuery.TYPE_STANDARD
            };
            PropertyQueryInfo query6 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N15422",
                OriginalTitle = "Elever i åk. 9 som är behöriga till ekonomi-, humanistiska och samhällsvetenskapsprogrammet, lägeskommun, andel (%)",
                Title = "Elever i åk. 9 som är behöriga till ekonomi-, humanistiska och samhällsvetenskapsprogrammet, lägeskommun, andel (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };

            OrganisationalUnitInfo ou1 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V15E129000301",
                Name = "Slättängsskolan LM",
                ShortDescription = "Slättängsskolan 7-9 är en högstadieskola med tre till fyra klasser i varje årskurs. Vi har i dagsläget ca 240 elever.",
                LongDescription = "Slättängsskolan 7-9 är en högstadieskola med tre till fyra klasser i varje årskurs. Vi har i dagsläget ca 240 elever. Skolan tillhör Kristianstads västra skolområde. Våra elever kommer från olika delar av skolområdet: Köpinge skola, Helgedalskola och Slättängskolan F-6.",
                ImagePath = "",
                Address = "Slättängsvägen 96, 291 62, Kristianstad",
                Telephone = "044-13 60 44",
                Contact = "Sven-Erik Nilsson, rektor",
                Email = "sven-erik.nilsson@utb.kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "www.kristianstad.se/Skolportaler/Slattangsskolan7-9/",
                Latitude = 56.019305,
                Longitude = 14.125117,
                Other = "Grundskola",
            };
            OrganisationalUnitInfo ou2 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V15E129000401",
                Name = "Nosabyskolan",
                ShortDescription = "Nosabyskolan hör till Skolområde Östra i Kristianstad. Upptagningsområdet består av blandad bebyggelse med höghus och villaområden i kommundelarna Hammar, Kulltorp och Österäng.",
                LongDescription = "Nosabyskolan hör till Skolområde Östra i Kristianstad. Upptagningsområdet består av blandad bebyggelse med höghus och villaområden i kommundelarna Hammar, Kulltorp och Österäng. Våra lokaler är moderna och väl anpassade dagens undervisning.De togs i bruk i januari 2013.",
                ImagePath = "https://www.kristianstad.se/ImageVaultFiles/id_7933/cf_2/223x937.jpg",
                Address = "Balsbyvägen 6, 29147, Kristianstad",
                Telephone = "044-136021",
                Contact = "Per Gustafsson, rektor",
                Email = "per.gustafsson@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "https://www.kristianstad.se/sv/Skolportaler/nosabyskolan/",
                Latitude = 56.052475,
                Longitude = 14.196342,
                Other = "Grundskola",
            };
            OrganisationalUnitInfo ou3 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V15E129000701",
                Name = "Öllsjöskolan LM",
                ShortDescription = "Skolan bedriver undervisning i F-9 och skolbarnomsorg för barn i åk F-6. Vi arbetar i två spår. I varje spår finns tre arbetslag indelade i F-3, 4-6 och 7-9. I arbetslagen F-6 ingår även verksamhet för barn i fritidshem och för elever i 7-9 finns verksamhet i elevcaféet.",
                LongDescription = "Skolan bedriver undervisning i F-9 och skolbarnomsorg för barn i åk F-6. Vi arbetar i två spår. I varje spår finns tre arbetslag indelade i F-3, 4-6 och 7-9. I arbetslagen F-6 ingår även verksamhet för barn i fritidshem och för elever i 7-9 finns verksamhet i elevcaféet. Det finns även ett arbetslag som arbetar för elevhälsan. På skolan finns engagerad personal med ett positivt och inkluderande förhållningssätt som vill ge eleverna en bra skola.Pedagogernas erfarenhet och kompetens tas tillvara för att nå en helhetssyn och hög kvalité.",
                ImagePath = "http://www.kristianstad.se/ImageVaultFiles/id_3650/cf_689/st_edited/B51DBx0ZxARwTj5f-xSo.jpg",
                Address = "Blåtands väg 20, 291 66, Kristianstad",
                Telephone = "044-134150",
                Contact = "Linus Pålsson, rektor",
                Email = "linus.palsson@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/sv/Skolportaler/ollsjoskolan/",
                Latitude = 56.011727,
                Longitude = 14.098495,
                Other = "Grundskola",
            };
            OrganisationalUnitInfo ou4 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V15E129002002",
                Name = "Sånnaskolan LM",
                ShortDescription = "Sånnaskolan är F-9-skolan med varm och vänlig atmosfär där elever och personal trivs och elevernas måluppfyllelse är hög.",
                LongDescription = "Sånnaskolan är F-9-skolan med varm och vänlig atmosfär där elever och personal trivs och elevernas måluppfyllelse är hög. Våra klasser är små för de yngre barnen och större för de äldre eftersom vi då tar emot elever från Villaskolan och Yngsjö skola.",
                ImagePath = "",
                Address = "Sandvaktaregatan 1, 29635, Åhus",
                Telephone = "0733-134651",
                Contact = "Marcus Svensson, rektor 7-9",
                Email = "marcus.svensson@utb.kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "https://www.kristianstad.se/sv/Skolportaler/sannaskolan/",
                Latitude = 55.9257,
                Longitude = 14.286111,
                Other = "Grundskola",
            };
            OrganisationalUnitInfo ou5 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V15E129002401",
                Name = "Tollarps skola L",
                ShortDescription = "Tollarps skola präglas av en god kollegial stämning, där det finns en öppenhet till nytänkande och utrymme för kreativitet. Vi har ett gott samarbete i personalgruppen och arbetar lösningsfokuserat tillsammans för att nå en hög måluppfyllelse.",
                LongDescription = "Tollarps skola präglas av en god kollegial stämning, där det finns en öppenhet till nytänkande och utrymme för kreativitet. Vi har ett gott samarbete i personalgruppen och arbetar lösningsfokuserat tillsammans för att nå en hög måluppfyllelse. Skolan är en F - 9 skola i kommunal regi som även bedriver fritidsverksamhet för årskurs F - 6.På skolan finns 415 elever i åldershomogena klasser.Dessa klasser är indelade i två spår, A och B.Kopplat till verksamheten finns tre arbetslag indelade i F - 3, 4 - 6 och 7 - 9.Det finns även ett arbetslag som arbetar för elevhälsan, EHT.I det sistnämnda arbetslaget finns speciallärare, specialpedagoger, skolsköterska, kurator och studie - och yrkesvägledare inklusive rektorerna.",
                ImagePath = "https://www.kristianstad.se/PageFiles/88603/d-huset3.jpg",
                Address = "Borgargatan 13, 298 33 Tollarp",
                Telephone = "044-134503",
                Contact = "Eva Axelsson, rektor 4-9",
                Email = "eva.axelsson@utb.kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "https://www.kristianstad.se/sv/Skolportaler/tollarpsskola/",
                Latitude = 55.931406,
                Longitude = 13.966987,
                Other = "Grundskola",
            };

            Category c1 = new Category()
            {
                Name = "Grundskola"
            };
            c1.Queries.Add(query1);
            c1.Queries.Add(query2);
            c1.Queries.Add(query3);
            c1.Queries.Add(query4);
            c1.Queries.Add(query5);
            c1.Queries.Add(query6);
            c1.OrganisationalUnits.Add(ou1);
            c1.OrganisationalUnits.Add(ou2);
            c1.OrganisationalUnits.Add(ou3);
            c1.OrganisationalUnits.Add(ou4);
            c1.OrganisationalUnits.Add(ou5);
            context.Categories.Add(c1);


            // GYMNASIESKOLA
            PropertyQueryInfo query7 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N17430",
                OriginalTitle = "Ungdomar som är etablerade på arbetsmarknaden 2 år efter fullföljd gymnasieutbildning, andel (%)",
                Title = "Ungdomar som är etablerade på arbetsmarknaden 2 år efter fullföljd gymnasieutbildning, andel (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };
            PropertyQueryInfo query8 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N17500",
                OriginalTitle = "Betygspoäng efter avslutad gymnasieutbildning hemkommun, genomsnitt",
                Title = "Betygspoäng efter avslutad gymnasieutbildning hemkommun, genomsnitt",
                Type = PropertyQuery.TYPE_STANDARD
            };
            PropertyQueryInfo query9 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N17444",
                OriginalTitle = "Gymnasieelever med examen eller studiebevis inom 3 år, kommunala skolor, andel (%)",
                Title = "Gymnasieelever med examen eller studiebevis inom 3 år, kommunala skolor, andel (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };
            PropertyQueryInfo query10 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "N17479",
                OriginalTitle = "Gymnasieelever som uppnått grundläggande behörighet till universitet och högskola inom 3 år, kommunala skolor, andel (%)",
                Title = "Gymnasieelever som uppnått grundläggande behörighet till universitet och högskola inom 3 år, kommunala skolor, andel (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };
            PropertyQueryInfo query11 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "U17025",
                OriginalTitle = "Dagliga resor & inackordering gymnasieskola, kr/elev",
                Title = "Dagliga resor & inackordering gymnasieskola, kr/elev",
                Type = PropertyQuery.TYPE_STANDARD
            };

            OrganisationalUnitInfo ou6 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "1290",
                Name = "Söderportgymnasiet 2",
                ShortDescription = "Söderportgymnasiet har detta läsår ca 800 elever. Skolan ligger i Kristianstads centrum, ca fem minuters gångväg från järnvägsstationen och är stadens äldsta gymnasium med pedagogik anpassad efter program och elever samt erfarna lärare.",
                LongDescription = "De största programmen på skolan är Samhällsvetenskapsprogrammet, där vi erbjuder flest inriktningar i kommunen, Ekonomiprogrammet, Handels- och Administrationsprogrammet, International Baccalaureate, Naturvetenskapsprogrammet, IM/ISPEC,Riksgymnasiet för rörelsehindrade och Gymnasiesärskolan.",
                ImagePath = "http://www.kristianstad.se/ImageVaultFiles/id_9356/cf_2483/st_edited/p518wZkmhF2Sw9Kcs9d2.jpg",
                Address = "Västra Boulevarden 53, 291 31 Kristianstad",
                Telephone = "044-13 52 91",
                Contact = "Ann-Sofi Ericson, rektor",
                Email = "ann-sofi.ericson@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/sv/Skolportaler/Soderport/",
                Latitude = 56.026218,
                Longitude = 14.156947,
                Other = "Gymnasieskola",
            };
            OrganisationalUnitInfo ou7 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V17E21702982",
                Name = "Österänggymnasiet",
                ShortDescription = "Österänggymnasiet är den största gymnasieskolan i Kristianstads kommun, både till antalet elever och till antal program du kan välja bland.",
                LongDescription = "Hos oss finns utbildningar som passar de flesta. Vi erbjuder tre högskoleför- beredande program samt fyra yrkesprogram. Vi erbjuder även många möjligheter inom individuellt val.",
                ImagePath = "http://www.kristianstad.se/upload/Osterang/Objects/Boxar/Bilder/fb_start_vinter.jpg",
                Address = "Österänggymnasiet, Sjövägen 30, 291 43 Kristianstad",
                Telephone = "044-136502",
                Contact = "Joakim Andersson, rektor Högskoleförberedande programmen",
                Email = "joakim.andersson@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/osteranggymnasiet",
                Latitude = 56.042175,
                Longitude = 14.175278,
                Other = "Gymnasieskola",
            };
            OrganisationalUnitInfo ou8 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V17E40766349",
                Name = "Christian 4:s Gymnasium 2",
                ShortDescription = "Christian 4:s Gymnasium är den enda skolan i Kristianstad med enbart högskoleförberedande program.",
                LongDescription = "Skolan ligger centralt i Kristianstad. Våra moderna men anrika lokaler har väl uppdaterad och ändamålsenlig teknisk utrustning. Trivseln på skolan är mycket hög och du som elev är med och påverkar det som händer på skolan och i din klass. Vi har en aktiv elevkår som hjälper oss att driva skolan framåt.",
                ImagePath = "http://www.kristianstad.se/ImageVaultFiles/id_4725/cf_2/C4-gymnasiet.jpg",
                Address = "Östra Boulevarden 13, 29153 Kristianstad",
                Telephone = "044135270",
                Contact = "Christel Lundin, rektor",
                Email = "christel.lundin@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/skolportaler/Christian-4s-Gymnasium1/",
                Latitude = 56.027327,
                Longitude = 14.161329,
                Other = "Gymnasieskola",
            };
            OrganisationalUnitInfo ou9 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V17E52705247",
                Name = "Lejongymnasiet",
                ShortDescription = "Lejongymnasiet är den lilla skolan med det stora mänskliga perspektivet.",
                LongDescription = "Från hösten 2011 startar vårt program, turism och resor för den som vill ha ut något mer av skolan och livet. Vår skola ligger i centrala Kristianstad mycket nära resecentrum och järnvägsstationen.",
                ImagePath = "",
                Address = "Östra Vallgatan 33-35, 291 31 Kristianstad",
                Telephone = "044-125080",
                Contact = "Anne Ryden, rektor",
                Email = "lejonskolan@telia.com",
                OrganisationalForm = "Kommunal",
                Website = "http://lejongymnasiet.se/",
                Latitude = 56.029153,
                Longitude = 14.15853,
                Other = "Gymnasieskola",
            };
            OrganisationalUnitInfo ou10 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V17E53691739",
                Name = "IT-Gymnasiet Kristianstad",
                ShortDescription = "IT-Gymnasiet Kristianstad startade 2010 och är det nyaste tillskottet bland de 11 IT-Gymnasierna som finns i Sverige. Vi bedriver IT-profilerade utbildningar och förbereder dig för framgångsrika studier efter gymnasiet och gör dig attraktiv på arbetsmarknaden.",
                LongDescription = "Vi har en kunskapssyn som vilar på att kunskap utvecklas bäst via praktisk handling i verkliga projekt där läraren samarbetar med dig för att ge dig goda kunskaper som hjälper dig i din lärandeprocess.",
                ImagePath = "https://scontent-bru2-1.xx.fbcdn.net/hphotos-xta1/v/t1.0-9/1655890_644536455614339_831798480_n.jpg?oh=f04dc51f5a8572f19521d0ba913f3128&oe=57945C36",
                Address = "Västra Vallgatan 2, 291 31 Kristianstad",
                Telephone = "044-20 59 80",
                Contact = "Helene Kjellgren, rektor",
                Email = "helene.kjellgren@it-gymnasiet.se",
                OrganisationalForm = "Kommunal",
                Website = "http://it-gymnasiet.se/vara-skolor/kristianstad/",
                Latitude = 56.030628,
                Longitude = 14.153876,
                Other = "Gymnasieskola",
            };

            Category c4 = new Category()
            {
                Name = "Gymnasieskola"
            };
            c4.Queries.Add(query7);
            c4.Queries.Add(query8);
            c4.Queries.Add(query9);
            c4.Queries.Add(query10);
            c4.Queries.Add(query11);
            c4.OrganisationalUnits.Add(ou6);
            c4.OrganisationalUnits.Add(ou7);
            c4.OrganisationalUnits.Add(ou8);
            c4.OrganisationalUnits.Add(ou9);
            c4.OrganisationalUnits.Add(ou10);
            context.Categories.Add(c4);

            GroupCategory gc1 = new GroupCategory()
            {
                Name = "Skola"
            };
            gc1.Categories.Add(c1);
            gc1.Categories.Add(c4);
            context.GroupCategories.Add(gc1);

            // SJUKVÅRD OCH HÄLSA
            Category c3 = new Category()
            {
                Name = "Sjukhus"
            };
            context.Categories.Add(c3);

            Category c5 = new Category()
            {
                Name = "Vårdcentral"
            };
            context.Categories.Add(c5);

            GroupCategory gc2 = new GroupCategory()
            {
                Name = "Sjukvård & hälsa"
            };
            gc2.Categories.Add(c3);
            gc2.Categories.Add(c5);
            context.GroupCategories.Add(gc2);


            // ÄLDREOMSORG
            GroupCategory gc3 = new GroupCategory()
            {
                Name = "Äldreomsorg"
            };
            Category c6 = new Category()
            {
                Name = "Vård- och omsorgsboende"
            };
            context.Categories.Add(c6);

            PropertyQueryInfo query12 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "U23468",
                OriginalTitle = "Brukarbedömning särskilt boende äldreomsorg - måltidsmiljö, andel (%)",
                Title = "Brukarbedömning särskilt boende äldreomsorg - måltidsmiljö, andel (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };
            PropertyQueryInfo query13 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "U23448",
                OriginalTitle = "Brukarbedömning särskilt boende äldreomsorg - hälsotillstånd, andel (%)",
                Title = "Brukarbedömning särskilt boende äldreomsorg - hälsotillstånd, andel (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };
            PropertyQueryInfo query14 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "U23496",
                OriginalTitle = "Omsorgspersonalen på helgdagar i boende med särskild service för äldre med adekvat utbildning, andel (%)",
                Title = "Omsorgspersonalen på helgdagar i boende med särskild service för äldre med adekvat utbildning, andel (%)",
                Type = PropertyQuery.TYPE_PERCENT
            };
            PropertyQueryInfo query15 = new PropertyQueryInfo()
            {
                WebServiceName = koladaWebServiceName,
                QueryId = "U23493",
                OriginalTitle = "Omsorgspersonal/plats i boende för särskild service för äldre, helgdagar, antal",
                Title = "Omsorgspersonal/plats i boende för särskild service för äldre, helgdagar, antal",
                Type = PropertyQuery.TYPE_STANDARD
            };

            OrganisationalUnitInfo ou11 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V23E1285",
                Name = "Almgården",
                ShortDescription = "Almgården är ett vård- och omsorgsboende och ligger i Tollarp. Alla lägenheterna ligger i markplan med trevlig utemiljö.",
                LongDescription = "Vi arbetar efter ett rehabiliteringstänkande som innebär hjälp till självhjälp. Det innebär att du som boende gör det du kan själv för att bibehålla eller förbättra dina funktioner. Vi personal stödjer, motiverar och stimulerar till träning och aktivitet i det dagliga livet. ",
                ImagePath = "http://www.kristianstad.se/upload/Malgrupper/Senior/boende/Almgarden_entre.jpg",
                Address = "Akasiavägen 18, 290 10 Tollarp",
                Telephone = "044 - 13 20 41",
                Contact = "Niklas Lindqvist, enhetschef",
                Email = "niklas.lindqvist@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/sv/Kristianstads-kommun/Vard-Omsorg/Boende/Boenden/Almgarden/",
                Latitude = 55.933026,
                Longitude = 13.988118,
                Other = "Hemtjänst",
            };

            OrganisationalUnitInfo ou12 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V23E1292",
                Name = "Hammarshemmet",
                ShortDescription = "Hammarshemmet är ett vård- och omsorgsboende som ligger i Hammar cirka 3 km utanför centrala Kristianstad.",
                LongDescription = "Vårt mål är att varje dag på Hammarshemmet ska bli glädjerik och meningsfull! Varje boendes behov är i centrum och genomförandeplaner utformas individuellt för att möta olika behov.",
                ImagePath = "http://www.kristianstad.se/upload/Malgrupper/Senior/boende/Hammarshemmet.jpg",
                Address = "Gamla Fjälkingevägen 9, 291 50 Kristianstad",
                Telephone = "044-13 48 68",
                Contact = "Niklas Lindqvist, enhetschef",
                Email = "niklas.lindqvist@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/sv/Kristianstads-kommun/Vard-Omsorg/Boende/Boenden/Hammarshemmet/",
                Latitude = 56.032068,
                Longitude = 14.21112,
                Other = "Hemtjänst",
            };

            OrganisationalUnitInfo ou13 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V23E1302",
                Name = "Allögården",
                ShortDescription = "Allögården C-hus är ett vård- och omsorgsboende som ligger centralt i Kristianstad.",
                LongDescription = "Vi strävar efter rätt kvalitet i varje möte, försöker skapa trivsel och trygghet och behandlar dig som boende med respekt. Vi arbetar för att du som boende själv ska bestämma över dina rutiner.",
                ImagePath = "http://www.kristianstad.se/upload/Malgrupper/Senior/boende/Allogarden.jpg",
                Address = "Kockumsgatan 5, 291 32 Kristianstad",
                Telephone = "044-13 55 75",
                Contact = "Niklas Lindqvist, enhetschef",
                Email = "niklas.lindqvist@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/sv/Kristianstads-kommun/Vard-Omsorg/Boende/Boenden/Allogarden/",
                Latitude = 56.038849,
                Longitude = 14.159851,
                Other = "Hemtjänst",
            };

            OrganisationalUnitInfo ou14 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V23E1310",
                Name = "Österängsgården",
                ShortDescription = "Österängsgården är ett vård- och omsorgsboende med olika inriktningar som ligger i det natursköna Österängsområdet med närhet till kommunikation och livsmedelsaffär. ",
                LongDescription = "Vår ambition är att ge omvårdnad utifrån dina enskilda behov och önskningar med fokus på det friska, att behålla var och ens förmåga så långt det är möjligt. Vi vill att du ska ges möjlighet till en god livskvalitet utifrån dina förutsättningar. Vi arbetar med dig som boende i centrum genom ett professionellt och empatiskt bemötande.",
                ImagePath = "http://www.kristianstad.se/upload/Malgrupper/Senior/boende/Osterangsgarden.jpg",
                Address = "Sjövägen 31 A, 291 45 Kristianstad",
                Telephone = "044-13 65 70",
                Contact = "Niklas Lindqvist, enhetschef",
                Email = "niklas.lindqvist@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/sv/Kristianstads-kommun/Vard-Omsorg/Boende/Boenden/Osterangsgarden/",
                Latitude = 56.042866,
                Longitude = 14.177657,
                Other = "Hemtjänst",
            };

            OrganisationalUnitInfo ou15 = new OrganisationalUnitInfo()
            {
                OrganisationalUnitId = "V23E1307",
                Name = "Almhaga",
                ShortDescription = "Almhaga är ett vård- och omsorgsboende för dig som behöver omfattande hjälp med service och omvårdnad. Boendet ligger centralt i Färlöv cirka 1 mil öster om Kristianstad.",
                LongDescription = "Kristianstads kommun har en lokal värdighetsgaranti för äldreomsorgen. Värdighetsgarantin innebär att kommunen har särskilda kvalitetsmål för några områden inom äldreomsorgen.",
                ImagePath = "http://www.kristianstad.se/upload/Malgrupper/Senior/boende/Almhaga.jpg",
                Address = "Grindvägen 12, 291 75 Färlöv",
                Telephone = "044-13 45 70",
                Contact = "Niklas Lindqvist, enhetschef",
                Email = "niklas.lindqvist@kristianstad.se",
                OrganisationalForm = "Kommunal",
                Website = "http://www.kristianstad.se/sv/Kristianstads-kommun/Vard-Omsorg/Boende/Boenden/Almhaga/",
                Latitude = 56.070933,
                Longitude = 14.085189,
                Other = "Hemtjänst",
            };

            c6.Queries.Add(query12);
            c6.Queries.Add(query13);
            c6.Queries.Add(query14);
            c6.Queries.Add(query15);
            c6.OrganisationalUnits.Add(ou11);
            c6.OrganisationalUnits.Add(ou12);
            c6.OrganisationalUnits.Add(ou13);
            c6.OrganisationalUnits.Add(ou14);
            c6.OrganisationalUnits.Add(ou15);


            gc3.Categories.Add(c6);
            context.GroupCategories.Add(gc3);

            #endregion
        }
    }
}
