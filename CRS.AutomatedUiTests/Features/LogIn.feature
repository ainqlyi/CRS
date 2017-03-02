Feature: LogIn
	In order to log in to the CRS CrispHealth website
	As a user
	I want to be able to login to the website

@LoginTests
Scenario Outline: Blacklisted users cannot login to the CRS website
	Given I navigate to 'http://crs.crisphealth.org'
	When I login as '<username>' and '<password>'
	Then I am not logged into the CRS website
	Examples:
	| username | password  |
	| BKatz    | pass#8014 |
	| CKegley  | pass#9024 |
	| FKlauka  | pass#2042 |
	| Gmerkle  | pass#8043 |
	| LHarne   | pass#2086 |
	| MNabers  | pass#0102 |
	| PWilson  | pass#2116 |
	| VChilds  | pass#0138 |

@LoginTests
Scenario Outline: Whitelisted users can login to the CRS website
	Given I navigate to 'http://crs.crisphealth.org'
	When I login as '<username>' and '<password>'
	Then I am logged into the CRS website
	Examples:
	| username    | password  |
	| ACunningham | Pass#3002 |
	| AHendricks  | Pass#4003 |
	| AHorton     | Pass#6004 |
	| AKnapp      | Pass#1005 |
	| AMaule      | Pass#9006 |
	| AMcCusker   | Pass#5007 |
	| AMcMullen   | Pass#5008 |
	| ARivers     | Pass#0009 |
	| AZanger     | pass#6010 |
	| Bbennighoff | pass#1011 |
	| BByers      | pass#0012 |
	| BKelly      | pass#5015 |
	| BNowakowski | pass#2017 |
	| BOliver     | pass#1018 |
	| BPhillips   | pass#4019 |
	| CBoothe     | pass#5020 |
	| CBurgess    | pass#7021 |
	| Ccobbs      | pass#1022 |
	| CColeman    | pass#7023 |
	| Clannen     | pass#6025 |
	| CSapp       | pass#8026 |
	| DAllen      | pass#3027 |
	| DBetlyon    | pass#8028 |
	| DBrooks     | pass#1029 |
	| DDodson     | pass#7030 |
	| DHarless    | pass#1031 |
	| DHernandez  | pass#0032 |
	| DHyman      | pass#0033 |
	| DRounds     | pass#1034 |
	| DRyan       | pass#3035 |
	| DSaraceno   | pass#1036 |
	| DSirk       | pass#2037 |
	| EBeranek    | pass#7038 |
	| ELangha     | pass#9039 |
	| ERice       | pass#6040 |
	| FCollins    | pass#9041 |
	| Hjacobs     | pass#6044 |
	| HSiskind    | pass#7045 |
	| JAcuna      | pass#9046 |
	| JBrant      | pass#4047 |
	| JBrinkley   | pass#1048 |
	| JCarroll    | pass#5049 |
	| JDeRyke     | pass#9050 |
	| JFigler     | pass#6051 |
	| JFofana     | pass#9052 |
	| JFogg       | pass#7053 |
	| JGordon     | pass#2054 |
	| JHall       | pass#2055 |
	| JHartman    | pass#7056 |
	| JHoward     | pass#7057 |
	| JHulvey     | pass#8059 |
	| JKeese      | pass#7060 |
	| Jmazzeo     | pass#7061 |
	| JMitchell   | pass#0062 |
	| JNagel      | pass#2063 |
	| JPeters     | pass#7064 |
	| JRaab       | pass#6065 |
	| JRotz       | pass#7066 |
	| JRowe       | pass#5067 |
	| JSmith      | pass#8068 |
	| JSwann      | pass#4069 |
	| JTaylor     | pass#5070 |
	| KEckert     | pass#9071 |
	| KFrazier    | pass#6072 |
	| KFridley    | pass#5073 |
	| KGootee     | pass#5074 |
	| Kharnish    | pass#2075 |
	| KJerome     | pass#9076 |
	| KJohnson    | pass#7077 |
	| KKearney    | pass#2078 |
	| KMulford    | pass#4079 |
	| KPulio      | pass#6080 |
	| KRing       | pass#9081 |
	| KRounsley   | pass#1082 |
	| KTalbot     | pass#0083 |
	| LDixon      | pass#2084 |
	| LHack       | pass#9085 |
	| LNelson     | pass#5087 |
	| LSnyder     | pass#2088 |
	| LTracey     | pass#5089 |
	| LVidal      | pass#4090 |
	| MBrozic     | pass#2091 |
	| MConnor     | pass#6092 |
	| MGajewski   | pass#6093 |
	| MGriffin    | pass#7095 |
	| MHerpel     | pass#8096 |
	| MKing       | pass#7097 |
	| MLBond      | pass#8098 |
	| MLomax      | pass#6099 |
	| MMorgan     | pass#8100 |
	| MMyers      | pass#0101 |
	| MPankey     | pass#6103 |
	| MPeacock    | pass#5104 |
	| MRecor      | pass#6105 |
	| MReiter     | pass#6106 |
	| MStefano    | pass#1107 |
	| MTaylor     | pass#8108 |
	| NSantiago   | pass#1109 |
	| PAllen      | pass#8110 |
	| PHurley     | pass#2111 |
	| PMarkunas   | pass#9112 |
	| PMcWhorter  | pass#2113 |
	| PRodriguez  | pass#8114 |
	| PStable     | pass#9115 |
	| RCarroll    | pass#1117 |
	| RCase       | pass#3118 |
	| REmrick     | pass#1119 |
	| RMott       | pass#3120 |
	| RPellegrino | pass#7121 |
	| RVaflor     | pass#9122 |
	| RWells      | pass#7123 |
	| RWhite      | pass#9124 |
	| SAnand      | pass#2125 |
	| SDawson     | pass#4126 |
	| SDohony     | pass#3127 |
	| SHendricks  | pass#2128 |
	| SLieb       | pass#7129 |
	| SLim        | pass#1130 |
	| SSchwarz    | pass#0131 |
	| TCalabria   | pass#2133 |
	| TCannady    | pass#9134 |
	| TChan       | pass#6135 |
	| TColes      | pass#4136 |
	| TKerr       | pass#1137 |
	| WZawwin     | pass#0139 |