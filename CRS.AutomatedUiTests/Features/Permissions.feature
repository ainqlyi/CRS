Feature: Permissions
	In order to restrict user access
	As a hospital user of CRS
	I want only be allowed to access specific folders

@Permissions
Scenario Outline: Users can only see valid casemix level folders
	Given I navigate to 'http://crs.crisphealth.org'
	When I login as '<username>' and '<password>'
	Then I am logged into the CRS website
	Then username '<username>' can see casemix folders for his permissions
	Then username '<username>' cannot see casemix folders outside his permission
	Examples:
	| username    | password  |
	|ABankoski	|pass$9033		|
	|ABauman	|pass&9128      |
	|ABurton	|Pass$2143      |
	|ACunningham|Pass#3002      |
	|ADeutschendorf|pass%9003   |
	|AEvenson	|Pass$3214      |
	|AHendricks	|Pass#4003      |
	|AHorton	|Pass#6004      |
	|Aknapp		|Pass#1005      |
	|ALara		|pass$8348      |
	|AMann		|pass^2512      |
	|AMaule		|Pass#9006      |
	|AMcCusker	|Pass#5007      |
	|AMcMullen	|Pass#5008      |
	|ANaik		|pass%4309      |
	|ARivers	|Pass#0009      |
	|ASchuster	|Pass$1423      |
	|AWang		|Pass$3214      |
	|AZanger	|pass#6010      |
	|Bbennighoff|pass#1011      |
	|BByers		|pass#0012      |
	|BDavis		|pass!9165      |
	|BKelly		|pass#5015      |
	|BNowakowski|pass#2017      |
	|BOliver	|pass#1018      |
	|BPhillips	|pass#4019      |
	|BYocubik	|pass&8734      |
	|CBoothe	|pass#5020      |
	|CBurgess	|pass#7021      |
	|CCimaszewski|Pass$3214     |
	|Cclark		|pass&9712      |
	|CCobbs		|pass#1022      |
	|CColeman	|pass#7023      |
	|CGizara	|pass+2816      |
	|Clannen	|pass#6025      |
	|CLeung		|pass-0219      |
	|CNottingham|pass%9001      |
	|CSapp		|pass#8026      |
	|CWilliams	|pass&8983      |
	|DAllen		|pass#3027      |
	|DBaker 	|pass%9009      |
	|DBetlyon	|pass#8028      |
	|DBrooks	|pass#1029      |
	|DCrawford	|pass#1892      |
	|DDodson	|pass#7030      |
	|DFeeney	|pass&5100      |
	|DHarless	|pass#1031      |
	|DHernandez	|pass#0032      |
	|DHyman		|pass#0033      |
	|DJohnson	|pass!2997      |
	|DKinzer	|pass&0951      |
	|DLucas		|Pass$3214      |
	|DRomans	|pass!8023      |
	|DRounds	|pass#1034      |
	|DRyan		|pass#3035      |
	|DSaraceno	|pass#1036      |
	|DSirk		|pass#2037      |
	|DWon		|pass%9007      |
	|DYoungquist|pass*6617      |
	|EBeranek	|pass#7038      |
	|EHaile		|Pass$1423      |
	|ELangha	|pass#9039      |
	|FCollins	|pass#9041      |
	|GAnyumba	|pass!4506      |
	|Hjacobs	|pass#6044      |
	|HSiskind	|pass#7045      |
	|IKatz		|pass^2513      |
	|JAcuna		|pass#9046      |
	|JAlpern	|Pass$3214      |
	|JBrant		|pass#4047      |
	|JBrinkley	|pass#1048      |
	|JCarroll	|pass#5049      |
	|JCraig		|Pass$3214      |
	|JDeRyke	|pass#9050      |
	|JDiehl		|pass%9005      |
	|JFiergang	|pass^2514      |
	|JFigler	|pass#6051      |
	|JFofana	|pass#9052      |
	|JFogg		|pass#7053      |
	|JGill		|pass%9006      |
	|JGordon	|pass#2054      |
	|JHall		|pass#2055      |
	|JHartman	|pass#7056      |
	|JHoward	|pass#7057      |
	|JHulvey	|pass#8059      |
	|JKeese		|pass#7060      |
	|Jmazzeo	|pass#7061      |
	|JMitchell	|pass#0062      |
	|JNagel		|pass#2063      |
	|JPeters	|pass#7064      |
	|JRaab		|pass#6065      |
	|JRotz		|pass#7066      |
	|JRowe		|pass#5067      |
	|JSmith		|pass#8068      |
	|JSwann		|pass#4069      |
	|JTaylor	|pass#5070      |
	|JTroyer	|pass&5105      |
	|JTucker	|pass%0427      |
	|KEckert	|pass#9071      |
	|KFeliciano	|pass-0247      |
	|KFrazier	|pass#6072      |
	|KFridley	|pass#5073      |
	|KGootee	|pass#5074      |
	|Kharnish	|pass#2075      |
	|KJerome	|pass#9076      |
	|KJohnson	|pass#7077      |
	|KKearney	|pass#2078      |
	|KMulford	|pass#4079      |
	|KPulio		|pass#6080      |
	|KRing		|pass#9081      |
	|KTalbot	|pass#0083      |
	|LDixon		|pass#2084      |
	|LDunaway	|pass^2511      |
	|LHack		|pass#9085      |
	|LKessler	|pass%9008      |
	|LNelson	|pass#5087      |
	|LSnyder	|pass#2088      |
	|LStrimel	|pass%3490      |
	|LTalbott	|pass+2817      |
	|LTracey	|pass#5089      |
	|LVidal		|pass#4090      |
	|MBrozic	|pass#2091      |
	|MConnor	|pass#6092      |
	|MEdelen	|pass#4751      |
	|MFrate		|pass+2818      |
	|MGajewski	|pass#6093      |
	|MGriffin	|pass#7095      |
	|MHerpel	|pass#8096      |
	|MJenkins	|pass!1037      |
	|MKing		|pass#7097      |
	|MLBond		|pass#8098      |
	|MLomax		|pass#6099      |
	|MMorgan	|pass#8100      |
	|MMyers		|pass#0101      |
	|MPankey	|pass#6103      |
	|MPeacock	|pass#5104      |
	|MPohl		|Pass$1432      |
	|MRecor		|pass#6105      |
	|MReiter	|pass#6106      |
	|MReynolds	|pass-0236      |
	|MSathiraju	|pass!3771      |
	|Msokolow	|pass-0209      |
	|MStefano	|pass#1107      |
	|MTaylor	|pass#8108      |
	|NSantiago	|pass#1109      |
	|NUdom		|pass&7743      |
	|OIbarra	|pass&3617      |
	|PAllen		|pass#8110      |
	|PHurley	|pass#2111      |
	|PMarkunas	|pass#9112      |
	|PMcWhorter	|pass#2113      |
	|PRodriguez	|pass#8114      |
	|PStable	|pass#9115      |
	|RCarroll	|pass#1117      |
	|RCase		|pass#3118      |
	|REmrick	|pass#1119      |
	|RKrothapalli|pass%9002     |
	|RMott		|pass#3120      |
	|RPellegrino|pass#7121      |
	|RVaflor	|pass#9122      |
	|RWells		|pass#7123      |
	|RWhite		|pass#9124      |
	|SAfzal		|Pass$3214      |
	|SAnand		|pass#2125      |
	|SAntony	|Pass$3214      |
	|SCalikoglu	|Pass$1423      |
	|SDawson	|pass#4126      |
	|SDohony	|pass#3127      |
	|SHendricks	|pass#2128      |
	|ShiBrown	|pass^2510      |
	|SLieb		|pass#7129      |
	|SLim		|pass#1130      |
	|SPorts		|pass&2853      |
	|SSchooley	|pass-9254      |
	|SSchwarz	|pass#0131      |
	|SShrestha	|Pass$3214      |
	|SVasudevan	|pass*1808      |
	|TBowman	|PassC@1528     |
	|TCalabria	|pass#2133      |
	|TCannady	|pass#9134      |
	|TChan		|pass#6135      |
	|TColes		|pass#4136      |
	|TKerr		|pass#1137      |
	|TSpringmann|pass%9004      |
	|WZawwin	|pass#0139      |
	|YHicks		|pass-0229      |
	|YPhillips	|pass+2715      |
	|Ywoldeye	|pass#1140      |
	|Zgoodling	|pass&4423      |     
	
	@Permissions
Scenario Outline: Users can only see valid system and hospital folders
	Given I navigate to 'http://crs.crisphealth.org'
	When I login as '<username>' and '<password>'
	Then I am logged into the CRS website
	Then username '<username>' can see the folders for his permissions
	Then username '<username>' cannot see the folders outside his permission
	Examples:
	| username    | password  |
	|210001testnonPHI	|$crisp#ai15	|
	|210001testPHI		|$crisp#ai15    |
	|210001testnonplusPHI|$crisp#ai15 |
	|210002testnonPHI	|$crisp#ai15    |
	|210002testPHI		|$crisp#ai15    |
	|210003testnonPHI	|$crisp#ai15    |
	|210003testPHI		|$crisp#ai15    |
	|210004testnonPHI	|$crisp#ai15    |
	|210004testPHI		|$crisp#ai15    |
	|210005testnonPHI	|$crisp#ai15    |
	|210005testPHI		|$crisp#ai15    |
	|210006testnonPHI	|$crisp#ai15    |
	|210006testPHI		|$crisp#ai15    |
	|210007testnonPHI	|$crisp#ai15    |
	|210007testPHI		|$crisp#ai15    |
	|210008testnonPHI	|$crisp#ai15    |
	|210008testPHI		|$crisp#ai15    |
	|210009testnonPHI	|$crisp#ai15    |
	|210009testPHI		|$crisp#ai15    |
	|210010testnonPHI	|$crisp#ai15    |
	|210010testPHI		|$crisp#ai15    |
	|210011testnonPHI	|$crisp#ai15    |
	|210011testPHI		|$crisp#ai15    |
	|210012testnonPHI	|$crisp#ai15    |
	|210012testPHI		|$crisp#ai15    |
	|210013testnonPHI	|$crisp#ai15    |
	|210013testPHI		|$crisp#ai15    |
	|210015testnonPHI	|$crisp#ai15    |
	|210015testPHI		|$crisp#ai15    |
	|210016testnonPHI	|$crisp#ai15    |
	|210016testPHI		|$crisp#ai15    |
	|210017testnonPHI	|$crisp#ai15    |
	|210017testPHI		|$crisp#ai15    |
	|210018testnonPHI	|$crisp#ai15    |
	|210018testPHI		|$crisp#ai15    |
	|210019testnonPHI	|$crisp#ai15    |
	|210019testPHI		|$crisp#ai15    |
	|210022testnonPHI	|$crisp#ai15    |
	|210022testPHI		|$crisp#ai15    |
	|210023testnonPHI	|$crisp#ai15    |
	|210023testPHI		|$crisp#ai15    |
	|210024testnonPHI	|$crisp#ai15    |
	|210024testPHI		|$crisp#ai15    |
	|210027testnonPHI	|$crisp#ai15    |
	|210027testPHI		|$crisp#ai15    |
	|210028testnonPHI	|$crisp#ai15    |
	|210028testPHI		|$crisp#ai15    |
	|210029testnonPHI	|$crisp#ai15    |
	|210029testPHI		|$crisp#ai15    |
	|210030testnonPHI	|$crisp#ai15    |
	|210030testPHI		|$crisp#ai15    |
	|210032testnonPHI	|$crisp#ai15    |
	|210032testPHI		|$crisp#ai15    |
	|210033testnonPHI	|$crisp#ai15    |
	|210033testPHI		|$crisp#ai15    |
	|210034testnonPHI	|$crisp#ai15    |
	|210034testPHI		|$crisp#ai15    |
	|210035testnonPHI	|$crisp#ai15    |
	|210035testPHI		|$crisp#ai15    |
	|210037testnonPHI	|$crisp#ai15    |
	|210037testPHI		|$crisp#ai15    |
	|210038testnonPHI	|$crisp#ai15    |
	|210038testPHI		|$crisp#ai15    |
	|210039testnonPHI	|$crisp#ai15    |
	|210039testPHI		|$crisp#ai15    |
	|210040testnonPHI	|$crisp#ai15    |
	|210040testPHI		|$crisp#ai15    |
	|210043testnonPHI	|$crisp#ai15    |
	|210043testPHI		|$crisp#ai15    |
	|210044testnonPHI	|$crisp#ai15    |
	|210044testPHI		|$crisp#ai15    |
	|210045testnonPHI	|$crisp#ai15    |
	|210045testPHI		|$crisp#ai15    |
	|210048testnonPHI	|$crisp#ai15    |
	|210048testPHI		|$crisp#ai15    |
	|210049testnonPHI	|$crisp#ai15    |
	|210049testPHI		|$crisp#ai15    |
	|210051testnonPHI	|$crisp#ai15    |
	|210051testPHI		|$crisp#ai15    |
	|210055testnonPHI	|$crisp#ai15    |
	|210055testPHI		|$crisp#ai15    |
	|210056testnonPHI	|$crisp#ai15    |
	|210056testPHI		|$crisp#ai15    |
	|210057testnonPHI	|$crisp#ai15    |
	|210057testPHI		|$crisp#ai15    |
	|210058testnonPHI	|$crisp#ai15    |
	|210058testPHI		|$crisp#ai15    |
	|210060testnonPHI	|$crisp#ai15    |
	|210060testPHI		|$crisp#ai15    |
	|210061testnonPHI	|$crisp#ai15    |
	|210061testPHI		|$crisp#ai15    |
	|210062testnonPHI	|$crisp#ai15    |
	|210062testPHI		|$crisp#ai15    |
	|210063testnonPHI	|$crisp#ai15    |
	|210063testPHI		|$crisp#ai15    |
	|210064testnonPHI	|$crisp#ai15    |
	|210064testPHI		|$crisp#ai15    |
	|210065testnonPHI	|$crisp#ai15    |
	|210065testPHI		|$crisp#ai15    |
	|210087testnonPHI	|$crisp#ai15    |
	|210087testPHI		|$crisp#ai15    |
	|210088testnonPHI	|$crisp#ai15    |
	|210088testPHI		|$crisp#ai15    |
	|210333testnonPHI	|$crisp#ai15    |
	|210333testPHI		|$crisp#ai15    |
	|210904testnonPHI	|$crisp#ai15    |
	|210904testPHI		|$crisp#ai15    |
	|212005testnonPHI	|$crisp#ai15    |
	|212005testPHI		|$crisp#ai15    |
	|213028testnonPHI	|$crisp#ai15    |
	|213028testPHI		|$crisp#ai15    |
	|213029testnonPHI	|$crisp#ai15    |
	|213029testPHI		|$crisp#ai15    |
	|213300testnonPHI	|$crisp#ai15    |
	|213300testPHI		|$crisp#ai15    |             