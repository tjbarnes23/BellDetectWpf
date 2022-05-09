﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        /*************************************************
        *   Note    Fstop1  Fpass1  Fpass2  Fstop2  Astop1  Apass   Astop2  Order
        *   PM B9   891     982     1002    1040    40      3       40      1366
        *************************************************/
        const int PM_B9_2_LEN = 1367;
        
        static readonly double[] pm_B9_2 = new double[PM_B9_2_LEN]
        {
            0.005242411619408,-0.0001979066791078,-0.0002007511611934,-0.0002071857674758,
  -0.0002176387614064,-0.0002309866950905,-0.0002475558213578,-0.000266114267306,
  -0.0002869100698895,-0.0003086386418488,-0.0003314929306337,-0.0003541156675005,
  -0.0003767006116241,-0.0003978559139148,-0.0004178307114546,-0.0004352326622867,
  -0.0004503891543249,-0.0004619560500282,-0.0004703951244295,-0.0004744199946194,
  -0.0004746583182646,-0.000469905694574,-0.0004610105231562,-0.0004468480101847,
  -0.0004285014476561,-0.0004049293915666,-0.0003774913066943,-0.0003451950294595,
  -0.0003096871014686,-0.0002699743502101,-0.0002280487502123,-0.0001828205830531,
  -0.0001366639489596,-8.827615951516e-05,-4.050994793458e-05, 8.38957009908e-06,
  5.482577650296e-05,0.0001015529208261,0.0001434370329613,0.0001862762903242,
  0.0002174860665094,0.0002524365449753,0.0002843259630253,0.0003033374893351,
  0.0003196742927993,0.0003279215996155, 0.000331783074532,0.0003279877445788,
  0.0003189779625638, 0.000302618705504,0.0002809262350184,0.0002524953274875,
  0.0002192004000548,0.0001801580623366,0.0001372001908173,8.984387433085e-05,
  3.989301113848e-05,-1.283649137323e-05,-6.654695482554e-05,-0.0001212258311061,
  -0.0001751184941922,-0.0002280988259482, -0.00027847352365,-0.000326080127847,
  -0.0003693522710583,-0.0004081672909093,-0.0004411102841314,-0.000468170734524,
  -0.0004881398300506,-0.0005011907774138,-0.0005063261901164,-0.0005039489958782,
  -0.0004933168595686,-0.0004750916771824,-0.0004487703030588,-0.0004153286219881,
  -0.0003744367806232,-0.0003274598412854,-0.0002741094356534,-0.0002163373773441,
  -0.0001535755870291,-8.715310077565e-05,-2.109024361683e-05,4.884613657866e-05,
  0.0001184153327914,0.0001869496093231,0.0002528728944677,0.0003154916680366,
  0.0003736070619044,0.0004264066216774,0.0004728570752054,0.0005121888150048,
  0.0005435815454241,0.0005664271793921,0.0005801941438348,0.0005845091821486,
  0.0005791765124789,0.0005641161639647,0.0005394602510724,0.0005054382398277,
   0.000462523766732,0.0004112504882063,0.0003523965959231,0.0002867576316811,
  0.0002153928522161,0.0001393244159456,5.981503382896e-05,-2.196046649234e-05,
  -0.0001045945289392,-0.0001868259033702,-0.0002671969050357,-0.0003444734506649,
  -0.0004172042620957,-0.0004842707532394,-0.0005443342586948,-0.0005964691734432,
  -0.0006395258217864,-0.0006728760613039,-0.0006955712920187, -0.00070744359445,
  -0.0007075335677778,-0.0006970002606822,-0.0006736580804487,-0.0006395912831399,
  -0.0005951609740829,-0.0005399906629626,-0.0004752582636718,-0.0004017234927083,
  -0.0003208094309513, -0.00023357826179,-0.0001415432320218,-4.597935889691e-05,
  5.147738152258e-05,0.0001494043573357,0.0002460818559203,0.0003400353370974,
  0.0004295657831181,0.0005132479673264, 0.000589528404664,0.0006571598123144,
  0.0007148177190785,0.0007615242462412,0.0007962777932937,0.0008184655801095,
  0.0008274658871174,0.0008230645854003,0.0008050715360196,0.0007737215075296,
  0.0007292610506881,0.0006723624565118,0.0006036950999083,0.0005243749973153,
  0.0004354382501198, 0.000338364778692,0.0002345121954207,0.0001256499153575,
  1.334104770824e-05,-0.000100416076685,-0.0002140148748233,-0.0003252754252165,
  -0.0004327830717727,-0.0005341152521135,-0.000628081951474,-0.0007132327176599,
  -0.000786971687353,-0.0008490313595692,-0.000897919553693,-0.0009328311790815,
  -0.0009527480238228,-0.000957316471904,-0.000946182759335,-0.0009195015069355,
  -0.0008774543204259,-0.0008206673875097,-0.0007498166970906,-0.0006659786329917,
  -0.0005703070948586,-0.0004642876546691,-0.0003494911228701,-0.0002277580886546,
  -0.0001009636759406,2.882308693828e-05,0.0001595443693224,0.0002890193415843,
  0.0004151428221348,0.0005357856221073,0.0006489398163516,0.0007526413932104,
   0.000845115478388,0.0009247101217767,0.0009900146916902, 0.001039778838076,
   0.001073047167065, 0.001089099933884, 0.001087499341077, 0.001068089822194,
   0.001031018875028,0.0009766977503213,0.0009058688319552,0.0008195046002964,
    0.00071887987538, 0.000605557262352,0.0004809686719813,0.0003477727801329,
  0.0002071793789859,6.184342992692e-05,-8.575686243044e-05,-0.0002333421254714,
  -0.0003785547211322,-0.0005190267787814,-0.0006523588374715,-0.0007762759182537,
  -0.0008886338345866,-0.0009874925001681,-0.001071104675868,-0.001137987608797,
  -0.001186877602107,-0.001216832263374,-0.001227164701366,-0.001217531017173,
   -0.00118788435764,-0.001138543897474,-0.001070122222884,-0.0009835841873533,
  -0.0008801614706408,-0.0007614286901289,-0.0006291786217519,-0.0004854801557157,
  -0.0003325620212058,-0.0001728775404988,-8.964162233645e-06,0.0001565037782949,
  0.0003208706112801,0.0004813790448741,0.0006354139385482,0.0007803513450225,
  0.0009137776652085,  0.00103336378016, 0.001137089900097, 0.001223035841755,
   0.001289770850206, 0.001335833361375, 0.001360628391762, 0.001363165608285,
   0.001343420992499, 0.001301783027244, 0.001238292103668, 0.001154045014791,
   0.001050204032571,0.0009284452144543, 0.000790493893911,0.0006385277318442,
   0.000474843760875,0.0003021127434274,0.0001230385191482,-5.941145403871e-05,
  -0.0002423358808638,-0.000422681268921,-0.0005975314167209,-0.0007639355664626,
  -0.0009191568948617,-0.001060509808399,-0.001185615446524,-0.001292244842681,
  -0.001378561152294, -0.00144294111858,-0.001484203143281,-0.001501453818842,
  -0.001494282364648,-0.001462579673094,-0.001406726376982,-0.001327411852434,
  -0.001225826872702,-0.001103415427333,-0.0009620750399829,-0.0008039518970379,
  -0.0006315530178889,-0.0004475591511213,-0.0002549639407122,-5.678813308505e-05,
  0.0001436647499389,0.0003432310640941,0.0005385280215225,0.0007263305137993,
  0.0009037281973206, 0.001067310395232, 0.001214633701151, 0.001343131578311,
   0.001450504007751, 0.001534804562283, 0.001594522494455, 0.001628549317304,
   0.001636158944375, 0.001617036966792, 0.001571301548265, 0.001499522998223,
   0.001402696957425,  0.00128226990264, 0.001140049485721,0.0009782525695364,
  0.0007993914844032,0.0006063024292367,0.0004020350041197,0.0001898854718487,
  -2.673357411866e-05,-0.0002442693979719,-0.0004591891126654,-0.0006679202144229,
  -0.0008670171306951,-0.001053121782644,-0.001223131046361,-0.001374132234347,
  -0.001503551958017,-0.001609098922333,-0.001688950669881,-0.001741586688272,
  -0.001766022753984,-0.001761652821365,-0.001728403962478,-0.001666619805425,
  -0.001577187707151,-0.001461345648464,-0.001320955542913,-0.001157972333816,
  -0.0009752443413299,-0.0007753796861627,-0.0005616189672963,-0.0003375421625327,
  -0.0001066549548906,0.0001273046074952,0.0003605153757804,0.0005890961539885,
  0.0008092636930839, 0.001017369224414, 0.001209946397432, 0.001383747978047,
   0.001535819596977, 0.001663529546416, 0.001764647312255,  0.00183736525797,
   0.001880333627212, 0.001892695273074, 0.001874090318142, 0.001824663201274,
     0.0017450581721, 0.001636416237741, 0.001500372900383, 0.001339023778476,
   0.001154880569353,0.0009508498581692,0.0007301849178491,0.0004964354194226,
  0.0002533747792754,4.935376963901e-06,-0.0002448068530073,-0.0004917613906182,
  -0.0007318683329324,-0.0009611345831004,-0.001175750346892,-0.001372119450888,
  -0.001546903031322,-0.001697167285566,-0.001820255129466,-0.001914102461411,
  -0.001976961203621,-0.002007595122533,-0.002005573053326,-0.001970508733883,
  -0.001902948748326,-0.001803839258429,-0.001674717049661,-0.001517483890966,
  -0.001334626703472,-0.001129005171806,-0.0009039587494026,-0.0006630573062845,
  -0.0004102294962514,-0.0001495296528313,0.0001147584736241,0.0003783359661037,
  0.0006368319808801,0.0008860010866303,  0.00112166492085, 0.001339920773458,
   0.001537076264273, 0.001709837763476, 0.001855230041099,  0.00197076560775,
   0.002054392783943, 0.002104632954535, 0.002120490292741, 0.002101580942772,
   0.002048042853116, 0.001960656025844, 0.001840691622934, 0.001689999797213,
    0.00151094039426, 0.001306352777757,  0.00107948171946,0.0008340041201834,
  0.0005738433722869,0.0003032647138297,2.664889718137e-05,-0.0002514682906328,
  -0.000526459666787,-0.0007939293062691,-0.001049259575804,-0.001288297224983,
    -0.0015071035477,-0.001701900517936,-0.001869416702678,-0.002006785020134,
  -0.002111684616078,-0.002182229631063,-0.002217142243612,-0.002215687307236,
  -0.002177795415626,-0.002103934002626, -0.00199523134646,-0.001853314368217,
  -0.001680434752119,-0.001479296731168, -0.00125314287422,-0.001005578770998,
  -0.0007406481015547,-0.0004626281117236,-0.0001760938986685,0.0001143086101082,
  0.0004037739351274,0.0006875593887874,0.0009609329656565, 0.001219396847423,
   0.001458609866494, 0.001674595211596, 0.001863667188296, 0.002022687762195,
   0.002148882614553, 0.002240102862204, 0.002294713955577, 0.002311721353496,
   0.002290701908767, 0.002231926552953, 0.002136182538851, 0.002005024720752,
   0.001840384839165, 0.001645010327345, 0.001421948238108, 0.001174758661987,
  0.0009075648968245,0.0006246430098714,0.0003305951534289, 3.02562896792e-05,
  -0.0002714180942778,-0.0005694511821282,-0.0008589434174823,-0.001135092769041,
  -0.001393303506153,-0.001629252288974,-0.001838988418466,-0.002018972489795,
  -0.002166171031576,-0.002278050844409,-0.002352686154872,-0.002388731126828,
  -0.002385496333252,-0.002342911802535,-0.002261587137029,-0.002142755554848,
  -0.001988305550915,-0.001800670867334,-0.001582876382921,-0.001338426160097,
  -0.001071311119784,-0.000785864382717,-0.0004867539003447,-0.0001788829121776,
  0.0001326576685826,0.0004427806269855,0.0007463113090732, 0.001038255058322,
   0.001313743844301, 0.001568191847016, 0.001797355287149, 0.001997381142477,
   0.002164897749794, 0.002297095811627,  0.00239160663308, 0.002446947856637,
   0.002461963974555, 0.002436370869828, 0.002370510709063, 0.002265403778886,
     0.0021226480652, 0.001944544032701,  0.00173394525505, 0.001494302966818,
   0.001229476812782,0.0009438116894642,0.0006419575621759,0.0003288969149288,
  9.765413627954e-06,-0.0003101517095807,-0.0006255917402881,-0.0009313054146752,
  -0.001222245540605,-0.001493552719022,-0.001740725353312,-0.001959603692521,
   -0.00214652126124,-0.002298309188163,-0.002412418169879,-0.002486868969871,
  -0.002520368105379,-0.002512263391703,-0.002462642355247,-0.002372233411526,
  -0.002242443569332,-0.002075363599069,-0.001873696413177,-0.001640687335636,
  -0.001380176941084, -0.00109638632128,-0.0007940045667131,-0.0004779881009196,
  -0.0001535341803757,0.0001739634752341,0.0004991608792381,0.0008166117694597,
    0.00112103973865, 0.001407475598927,  0.00167104867943, 0.001907398600511,
   0.002112549560586, 0.002283105487147, 0.002416140756753, 0.002509415583176,
   0.002561293225832, 0.002570898520557, 0.002537969911823, 0.002463024760138,
   0.002347197816736, 0.002192381852144, 0.002001052396524,  0.00177635827469,
   0.001521938810289, 0.001242003991469,0.0009411186384403,0.0006242717491595,
  0.0002966441347019,-3.631787044742e-05,-0.0003691277376364,-0.0006962346041575,
  -0.001012252375318, -0.00131190194594,-0.001590225405038,-0.001842534503691,
  -0.002064678027129,-0.002252896409995,-0.002404033918419,-0.002515545333537,
  -0.002585534814168,-0.002612772593696,-0.002596791322157,-0.002537749973615,
  -0.002436641381051,-0.002295024301435,-0.002115254956628,-0.001900258129562,
  -0.001653505248508,-0.001379125127183,-0.001081606731278,-0.0007658224246803,
  -0.0004370214113951,-0.0001006398164719,0.0002377391630069,0.0005725404151819,
  0.0008981916189701, 0.001209288967841,  0.00150063878698, 0.001767412429762,
   0.002005142789035, 0.002209883904074, 0.002378178502018, 0.002507223305927,
   0.002594817865299, 0.002639490588011, 0.002640435371968, 0.002597621630352,
    0.00251169843159,  0.00238408471674, 0.002216820416545, 0.002012665890662,
   0.001774953921733, 0.001507636612943, 0.001215093973141,0.0009021718223063,
   0.000574024434028,0.0002361409564336,-0.0001059593217281,-0.0004465402153461,
  -0.0007799838760157,-0.001100738163067,-0.001403473698785,-0.001683149798902,
  -0.001935119537243,-0.002155149309615,-0.002339620856249,-0.002485356170506,
  -0.002590018004318,-0.002651764657698,-0.002669553454354,-0.002643079967025,
  -0.002572760827747,-0.002459696482151,-0.002305754897379,-0.002113457954731,
  -0.001886003070791,-0.001627112592652, -0.00134108025863,-0.001032623765684,
  -0.0007068712334682,-0.000369205356843,-2.524040619186e-05,0.0003193382245457,
  0.0006587866645109,0.0009874866164456, 0.001299957578259,  0.00159101923737,
    0.00185580832335, 0.002089924900994, 0.002289455429307, 0.002451099236954,
   0.002572131515805, 0.002650529789186, 0.002684952345969, 0.002674837358922,
   0.002620326537636, 0.002522285293976, 0.002382336202757,     0.00220281679,
   0.001986632091403, 0.001737421978355, 0.001459259986022, 0.001156789998718,
  0.0008350107342098,0.0004992574968301,0.0001551159384377,-0.0001917164120319,
  -0.0005354842454047,-0.0008704213408212,-0.001191056122887,-0.001491976903074,
  -0.001768216142387, -0.00201516041433,-0.002228741428212,-0.002405356883823,
  -0.002542086466794,-0.002636620070303,-0.002687422976191,-0.002693602633716,
  -0.002655078334742,-0.002572445925536,-0.002447102288695,-0.002281094070218,
  -0.002077199041972,-0.001838765150142,-0.001569782098834,-0.001274681224922,
  -0.0009583906252513,-0.0006261268974747,-0.0002834363730193,6.400391006973e-05,
  0.0004103955796161,0.0007500243607347, 0.001077227086155, 0.001386585450029,
   0.001672914100415, 0.001931504396102, 0.002158047294761, 0.002348745645363,
   0.002500465673024, 0.002610680255575, 0.002677535160514, 0.002699961743913,
   0.002677535160514, 0.002610680255575, 0.002500465673024, 0.002348745645363,
   0.002158047294761, 0.001931504396102, 0.001672914100415, 0.001386585450029,
   0.001077227086155,0.0007500243607347,0.0004103955796161,6.400391006973e-05,
  -0.0002834363730193,-0.0006261268974747,-0.0009583906252513,-0.001274681224922,
  -0.001569782098834,-0.001838765150142,-0.002077199041972,-0.002281094070218,
  -0.002447102288695,-0.002572445925536,-0.002655078334742,-0.002693602633716,
  -0.002687422976191,-0.002636620070303,-0.002542086466794,-0.002405356883823,
  -0.002228741428212, -0.00201516041433,-0.001768216142387,-0.001491976903074,
  -0.001191056122887,-0.0008704213408212,-0.0005354842454047,-0.0001917164120319,
  0.0001551159384377,0.0004992574968301,0.0008350107342098, 0.001156789998718,
   0.001459259986022, 0.001737421978355, 0.001986632091403,     0.00220281679,
   0.002382336202757, 0.002522285293976, 0.002620326537636, 0.002674837358922,
   0.002684952345969, 0.002650529789186, 0.002572131515805, 0.002451099236954,
   0.002289455429307, 0.002089924900994,  0.00185580832335,  0.00159101923737,
   0.001299957578259,0.0009874866164456,0.0006587866645109,0.0003193382245457,
  -2.524040619186e-05,-0.000369205356843,-0.0007068712334682,-0.001032623765684,
   -0.00134108025863,-0.001627112592652,-0.001886003070791,-0.002113457954731,
  -0.002305754897379,-0.002459696482151,-0.002572760827747,-0.002643079967025,
  -0.002669553454354,-0.002651764657698,-0.002590018004318,-0.002485356170506,
  -0.002339620856249,-0.002155149309615,-0.001935119537243,-0.001683149798902,
  -0.001403473698785,-0.001100738163067,-0.0007799838760157,-0.0004465402153461,
  -0.0001059593217281,0.0002361409564336, 0.000574024434028,0.0009021718223063,
   0.001215093973141, 0.001507636612943, 0.001774953921733, 0.002012665890662,
   0.002216820416545,  0.00238408471674,  0.00251169843159, 0.002597621630352,
   0.002640435371968, 0.002639490588011, 0.002594817865299, 0.002507223305927,
   0.002378178502018, 0.002209883904074, 0.002005142789035, 0.001767412429762,
    0.00150063878698, 0.001209288967841,0.0008981916189701,0.0005725404151819,
  0.0002377391630069,-0.0001006398164719,-0.0004370214113951,-0.0007658224246803,
  -0.001081606731278,-0.001379125127183,-0.001653505248508,-0.001900258129562,
  -0.002115254956628,-0.002295024301435,-0.002436641381051,-0.002537749973615,
  -0.002596791322157,-0.002612772593696,-0.002585534814168,-0.002515545333537,
  -0.002404033918419,-0.002252896409995,-0.002064678027129,-0.001842534503691,
  -0.001590225405038, -0.00131190194594,-0.001012252375318,-0.0006962346041575,
  -0.0003691277376364,-3.631787044742e-05,0.0002966441347019,0.0006242717491595,
  0.0009411186384403, 0.001242003991469, 0.001521938810289,  0.00177635827469,
   0.002001052396524, 0.002192381852144, 0.002347197816736, 0.002463024760138,
   0.002537969911823, 0.002570898520557, 0.002561293225832, 0.002509415583176,
   0.002416140756753, 0.002283105487147, 0.002112549560586, 0.001907398600511,
    0.00167104867943, 0.001407475598927,  0.00112103973865,0.0008166117694597,
  0.0004991608792381,0.0001739634752341,-0.0001535341803757,-0.0004779881009196,
  -0.0007940045667131, -0.00109638632128,-0.001380176941084,-0.001640687335636,
  -0.001873696413177,-0.002075363599069,-0.002242443569332,-0.002372233411526,
  -0.002462642355247,-0.002512263391703,-0.002520368105379,-0.002486868969871,
  -0.002412418169879,-0.002298309188163, -0.00214652126124,-0.001959603692521,
  -0.001740725353312,-0.001493552719022,-0.001222245540605,-0.0009313054146752,
  -0.0006255917402881,-0.0003101517095807,9.765413627954e-06,0.0003288969149288,
  0.0006419575621759,0.0009438116894642, 0.001229476812782, 0.001494302966818,
    0.00173394525505, 0.001944544032701,   0.0021226480652, 0.002265403778886,
   0.002370510709063, 0.002436370869828, 0.002461963974555, 0.002446947856637,
    0.00239160663308, 0.002297095811627, 0.002164897749794, 0.001997381142477,
   0.001797355287149, 0.001568191847016, 0.001313743844301, 0.001038255058322,
  0.0007463113090732,0.0004427806269855,0.0001326576685826,-0.0001788829121776,
  -0.0004867539003447,-0.000785864382717,-0.001071311119784,-0.001338426160097,
  -0.001582876382921,-0.001800670867334,-0.001988305550915,-0.002142755554848,
  -0.002261587137029,-0.002342911802535,-0.002385496333252,-0.002388731126828,
  -0.002352686154872,-0.002278050844409,-0.002166171031576,-0.002018972489795,
  -0.001838988418466,-0.001629252288974,-0.001393303506153,-0.001135092769041,
  -0.0008589434174823,-0.0005694511821282,-0.0002714180942778, 3.02562896792e-05,
  0.0003305951534289,0.0006246430098714,0.0009075648968245, 0.001174758661987,
   0.001421948238108, 0.001645010327345, 0.001840384839165, 0.002005024720752,
   0.002136182538851, 0.002231926552953, 0.002290701908767, 0.002311721353496,
   0.002294713955577, 0.002240102862204, 0.002148882614553, 0.002022687762195,
   0.001863667188296, 0.001674595211596, 0.001458609866494, 0.001219396847423,
  0.0009609329656565,0.0006875593887874,0.0004037739351274,0.0001143086101082,
  -0.0001760938986685,-0.0004626281117236,-0.0007406481015547,-0.001005578770998,
   -0.00125314287422,-0.001479296731168,-0.001680434752119,-0.001853314368217,
   -0.00199523134646,-0.002103934002626,-0.002177795415626,-0.002215687307236,
  -0.002217142243612,-0.002182229631063,-0.002111684616078,-0.002006785020134,
  -0.001869416702678,-0.001701900517936,  -0.0015071035477,-0.001288297224983,
  -0.001049259575804,-0.0007939293062691,-0.000526459666787,-0.0002514682906328,
  2.664889718137e-05,0.0003032647138297,0.0005738433722869,0.0008340041201834,
    0.00107948171946, 0.001306352777757,  0.00151094039426, 0.001689999797213,
   0.001840691622934, 0.001960656025844, 0.002048042853116, 0.002101580942772,
   0.002120490292741, 0.002104632954535, 0.002054392783943,  0.00197076560775,
   0.001855230041099, 0.001709837763476, 0.001537076264273, 0.001339920773458,
    0.00112166492085,0.0008860010866303,0.0006368319808801,0.0003783359661037,
  0.0001147584736241,-0.0001495296528313,-0.0004102294962514,-0.0006630573062845,
  -0.0009039587494026,-0.001129005171806,-0.001334626703472,-0.001517483890966,
  -0.001674717049661,-0.001803839258429,-0.001902948748326,-0.001970508733883,
  -0.002005573053326,-0.002007595122533,-0.001976961203621,-0.001914102461411,
  -0.001820255129466,-0.001697167285566,-0.001546903031322,-0.001372119450888,
  -0.001175750346892,-0.0009611345831004,-0.0007318683329324,-0.0004917613906182,
  -0.0002448068530073,4.935376963901e-06,0.0002533747792754,0.0004964354194226,
  0.0007301849178491,0.0009508498581692, 0.001154880569353, 0.001339023778476,
   0.001500372900383, 0.001636416237741,   0.0017450581721, 0.001824663201274,
   0.001874090318142, 0.001892695273074, 0.001880333627212,  0.00183736525797,
   0.001764647312255, 0.001663529546416, 0.001535819596977, 0.001383747978047,
   0.001209946397432, 0.001017369224414,0.0008092636930839,0.0005890961539885,
  0.0003605153757804,0.0001273046074952,-0.0001066549548906,-0.0003375421625327,
  -0.0005616189672963,-0.0007753796861627,-0.0009752443413299,-0.001157972333816,
  -0.001320955542913,-0.001461345648464,-0.001577187707151,-0.001666619805425,
  -0.001728403962478,-0.001761652821365,-0.001766022753984,-0.001741586688272,
  -0.001688950669881,-0.001609098922333,-0.001503551958017,-0.001374132234347,
  -0.001223131046361,-0.001053121782644,-0.0008670171306951,-0.0006679202144229,
  -0.0004591891126654,-0.0002442693979719,-2.673357411866e-05,0.0001898854718487,
  0.0004020350041197,0.0006063024292367,0.0007993914844032,0.0009782525695364,
   0.001140049485721,  0.00128226990264, 0.001402696957425, 0.001499522998223,
   0.001571301548265, 0.001617036966792, 0.001636158944375, 0.001628549317304,
   0.001594522494455, 0.001534804562283, 0.001450504007751, 0.001343131578311,
   0.001214633701151, 0.001067310395232,0.0009037281973206,0.0007263305137993,
  0.0005385280215225,0.0003432310640941,0.0001436647499389,-5.678813308505e-05,
  -0.0002549639407122,-0.0004475591511213,-0.0006315530178889,-0.0008039518970379,
  -0.0009620750399829,-0.001103415427333,-0.001225826872702,-0.001327411852434,
  -0.001406726376982,-0.001462579673094,-0.001494282364648,-0.001501453818842,
  -0.001484203143281, -0.00144294111858,-0.001378561152294,-0.001292244842681,
  -0.001185615446524,-0.001060509808399,-0.0009191568948617,-0.0007639355664626,
  -0.0005975314167209,-0.000422681268921,-0.0002423358808638,-5.941145403871e-05,
  0.0001230385191482,0.0003021127434274, 0.000474843760875,0.0006385277318442,
   0.000790493893911,0.0009284452144543, 0.001050204032571, 0.001154045014791,
   0.001238292103668, 0.001301783027244, 0.001343420992499, 0.001363165608285,
   0.001360628391762, 0.001335833361375, 0.001289770850206, 0.001223035841755,
   0.001137089900097,  0.00103336378016,0.0009137776652085,0.0007803513450225,
  0.0006354139385482,0.0004813790448741,0.0003208706112801,0.0001565037782949,
  -8.964162233645e-06,-0.0001728775404988,-0.0003325620212058,-0.0004854801557157,
  -0.0006291786217519,-0.0007614286901289,-0.0008801614706408,-0.0009835841873533,
  -0.001070122222884,-0.001138543897474, -0.00118788435764,-0.001217531017173,
  -0.001227164701366,-0.001216832263374,-0.001186877602107,-0.001137987608797,
  -0.001071104675868,-0.0009874925001681,-0.0008886338345866,-0.0007762759182537,
  -0.0006523588374715,-0.0005190267787814,-0.0003785547211322,-0.0002333421254714,
  -8.575686243044e-05,6.184342992692e-05,0.0002071793789859,0.0003477727801329,
  0.0004809686719813, 0.000605557262352,  0.00071887987538,0.0008195046002964,
  0.0009058688319552,0.0009766977503213, 0.001031018875028, 0.001068089822194,
   0.001087499341077, 0.001089099933884, 0.001073047167065, 0.001039778838076,
  0.0009900146916902,0.0009247101217767, 0.000845115478388,0.0007526413932104,
  0.0006489398163516,0.0005357856221073,0.0004151428221348,0.0002890193415843,
  0.0001595443693224,2.882308693828e-05,-0.0001009636759406,-0.0002277580886546,
  -0.0003494911228701,-0.0004642876546691,-0.0005703070948586,-0.0006659786329917,
  -0.0007498166970906,-0.0008206673875097,-0.0008774543204259,-0.0009195015069355,
  -0.000946182759335,-0.000957316471904,-0.0009527480238228,-0.0009328311790815,
  -0.000897919553693,-0.0008490313595692,-0.000786971687353,-0.0007132327176599,
  -0.000628081951474,-0.0005341152521135,-0.0004327830717727,-0.0003252754252165,
  -0.0002140148748233,-0.000100416076685,1.334104770824e-05,0.0001256499153575,
  0.0002345121954207, 0.000338364778692,0.0004354382501198,0.0005243749973153,
  0.0006036950999083,0.0006723624565118,0.0007292610506881,0.0007737215075296,
  0.0008050715360196,0.0008230645854003,0.0008274658871174,0.0008184655801095,
  0.0007962777932937,0.0007615242462412,0.0007148177190785,0.0006571598123144,
   0.000589528404664,0.0005132479673264,0.0004295657831181,0.0003400353370974,
  0.0002460818559203,0.0001494043573357,5.147738152258e-05,-4.597935889691e-05,
  -0.0001415432320218, -0.00023357826179,-0.0003208094309513,-0.0004017234927083,
  -0.0004752582636718,-0.0005399906629626,-0.0005951609740829,-0.0006395912831399,
  -0.0006736580804487,-0.0006970002606822,-0.0007075335677778, -0.00070744359445,
  -0.0006955712920187,-0.0006728760613039,-0.0006395258217864,-0.0005964691734432,
  -0.0005443342586948,-0.0004842707532394,-0.0004172042620957,-0.0003444734506649,
  -0.0002671969050357,-0.0001868259033702,-0.0001045945289392,-2.196046649234e-05,
  5.981503382896e-05,0.0001393244159456,0.0002153928522161,0.0002867576316811,
  0.0003523965959231,0.0004112504882063, 0.000462523766732,0.0005054382398277,
  0.0005394602510724,0.0005641161639647,0.0005791765124789,0.0005845091821486,
  0.0005801941438348,0.0005664271793921,0.0005435815454241,0.0005121888150048,
  0.0004728570752054,0.0004264066216774,0.0003736070619044,0.0003154916680366,
  0.0002528728944677,0.0001869496093231,0.0001184153327914,4.884613657866e-05,
  -2.109024361683e-05,-8.715310077565e-05,-0.0001535755870291,-0.0002163373773441,
  -0.0002741094356534,-0.0003274598412854,-0.0003744367806232,-0.0004153286219881,
  -0.0004487703030588,-0.0004750916771824,-0.0004933168595686,-0.0005039489958782,
  -0.0005063261901164,-0.0005011907774138,-0.0004881398300506,-0.000468170734524,
  -0.0004411102841314,-0.0004081672909093,-0.0003693522710583,-0.000326080127847,
   -0.00027847352365,-0.0002280988259482,-0.0001751184941922,-0.0001212258311061,
  -6.654695482554e-05,-1.283649137323e-05,3.989301113848e-05,8.984387433085e-05,
  0.0001372001908173,0.0001801580623366,0.0002192004000548,0.0002524953274875,
  0.0002809262350184, 0.000302618705504,0.0003189779625638,0.0003279877445788,
   0.000331783074532,0.0003279215996155,0.0003196742927993,0.0003033374893351,
  0.0002843259630253,0.0002524365449753,0.0002174860665094,0.0001862762903242,
  0.0001434370329613,0.0001015529208261,5.482577650296e-05, 8.38957009908e-06,
  -4.050994793458e-05,-8.827615951516e-05,-0.0001366639489596,-0.0001828205830531,
  -0.0002280487502123,-0.0002699743502101,-0.0003096871014686,-0.0003451950294595,
  -0.0003774913066943,-0.0004049293915666,-0.0004285014476561,-0.0004468480101847,
  -0.0004610105231562,-0.000469905694574,-0.0004746583182646,-0.0004744199946194,
  -0.0004703951244295,-0.0004619560500282,-0.0004503891543249,-0.0004352326622867,
  -0.0004178307114546,-0.0003978559139148,-0.0003767006116241,-0.0003541156675005,
  -0.0003314929306337,-0.0003086386418488,-0.0002869100698895,-0.000266114267306,
  -0.0002475558213578,-0.0002309866950905,-0.0002176387614064,-0.0002071857674758,
  -0.0002007511611934,-0.0001979066791078, 0.005242411619408
        };
    }
}