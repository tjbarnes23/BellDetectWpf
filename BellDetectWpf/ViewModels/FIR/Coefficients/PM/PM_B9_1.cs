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
        * PM B9; 1048-1098-1335-1385 Hz; 592 order; 44.1 kHz; -30/-5 dB
        *************************************************/
        const int PM_B9_1_LEN = 593;
        
        static readonly double[] pm_B9_1 = new double[PM_B9_1_LEN]
        {
            0.01445480961051,-0.009129925090438,-0.006861650411119,-0.005265836073165,
          -0.004137175915421, -0.00333102711918,-0.002741540006568,-0.002295978177131,
          -0.001940456517966,-0.001639963187201,-0.001368316128307,-0.001110921500459,
          -0.0008567426948171,-0.0006024397767347,-0.0003455057027712,-8.878186361588e-05,
          0.0001654675875309,0.0004112228351153,0.0006444141952839,0.0008574960447962,
           0.001046163863749, 0.001203331032189, 0.001325734073159, 0.001407737418139,
           0.001448092730437, 0.001443250934962, 0.001394789911033, 0.001301743162031,
           0.001168561387411,0.0009967845354258,0.0007935326686037,0.0005625427027526,
           0.000313244434114,5.050630305998e-05,-0.0002145660363467,-0.000477042453559,
          -0.0007252727610012,-0.0009549289950061,-0.001154859561794,-0.001322994659248,
           -0.00144915473127,-0.001534694688966,-0.001570248438616, -0.00156258965523,
           -0.00150214700297, -0.00140112824179,  -0.0012570664366,-0.001063339026327,
          -0.0008662573957286,-0.0006070280841274,-0.0003527880363675,-9.146414767987e-05,
          0.0001805618164712,0.0004512351940108,0.0006939874271298,0.0009131752224675,
            0.00110657552397, 0.001270904150961, 0.001390318091089,  0.00146287770043,
           0.001487507128069, 0.001471300835506, 0.001412647743263, 0.001313343898011,
           0.001172207178198,0.0009969465877898,0.0007936022932914,0.0005735877835598,
          0.0003424222129859,0.0001083373794174,-0.0001247436297867,-0.0003481488898286,
          -0.0005564510353442,-0.0007401254899195,-0.0008949197372924,-0.001014439843217,
          -0.001098295937037,-0.001144044675244,-0.001154305367947,-0.001128410175202,
          -0.001070497428543,-0.0009808822233211,-0.0008659771915571,-0.0007281061486427,
          -0.0005758450874867, -0.00041310638305,-0.0002495174875532,-8.837645163121e-05,
          6.099090400967e-05, 0.000197805710509,0.0003132891349988,0.0004105728745723,
          0.0004813395343902,0.0005297459039242,0.0005545141437316,0.0005508307141623,
           0.000533330735982,0.0004902446405076, 0.000433895456335,0.0003692008908391,
          0.0002971260953794,0.0002219559720448,0.0001545616610582,9.576956609821e-05,
          4.788527761347e-05,1.485849328762e-05,1.900359632057e-06,7.252263286112e-06,
           2.96570145519e-05, 6.79687441777e-05,0.0001221954115939,0.0001882781647909,
          0.0002609987354465,0.0003343852207291,0.0004050329491426,0.0004679034339563,
          0.0005175382138493,0.0005473680166592,0.0005528535252233,0.0005302677176521,
          0.0004782778448803,0.0003947927789294,0.0002795527292128, 0.000132926358846,
          -4.164997075235e-05, -0.00023946892118,-0.0004535311557704,-0.0006774895176468,
           -0.00090267008712,-0.001121532125451,-0.001323219622679,-0.001499002763076,
          -0.001638139889512, -0.00173331050329, -0.00177580494226,-0.001761697915139,
          -0.001684786058852,-0.001545470717307,-0.001340029924093,-0.001074666634451,
          -0.0007505706989655,-0.000377356073866,3.441822677622e-05,0.0004769069734007,
           0.000931260091994, 0.001387516590351, 0.001829230957443, 0.002238794062014,
           0.002601275661071, 0.002903331511417, 0.003128042299722, 0.003264601645696,
           0.003304466510038, 0.003240955475742, 0.003068875099082,  0.00278993040117,
           0.002407805327558, 0.001929131448636, 0.001363621065206, 0.000726628325763,
           3.56281409242e-05,-0.0006892980467495,-0.001427322869883,-0.002154468962661,
          -0.002846244635872,-0.003478709016795,-0.004030154141029,-0.004479028056615,
          -0.004806731336767,-0.004996971589208,-0.005038512037442,-0.004924284598943,
          -0.004651891368814,-0.004223219154112,-0.003646254743997,-0.002933281187277,
          -0.002103235590706, -0.00117728895739,-0.0001821811649135,0.0008544673703095,
           0.001900506497928, 0.002924124545023, 0.003890756631414, 0.004769825651968,
           0.005529086007911, 0.006143484642276,  0.00658581106705, 0.006839828120587,
           0.006889247348798, 0.006727503766789, 0.006354237535557, 0.005773119714807,
           0.004998194891376, 0.004047460383497, 0.002944464348363, 0.001720916238242,
          0.0004114639355501,-0.000946575977579,-0.002311153605604, -0.00363966894867,
          -0.004891183795439,-0.006024806636539,-0.007001386713209, -0.00778709706701,
          -0.008353558509567,-0.008677257685444,-0.008742476624543,-0.008541777407816,
           -0.00807549544492,-0.007351615894703,-0.006386847873759,-0.005205731108713,
          -0.003840811408387,-0.002329837301135,-0.000716051824604,0.0009524477417658,
           0.002624878849009,  0.00425047750763, 0.005778074245991, 0.007159377503369,
            0.00834817928906, 0.009304500158191, 0.009994854329373,  0.01039316110463,
            0.01048221668708,  0.01025335498467, 0.009708685221558, 0.008859478083449,
           0.007727103801067, 0.006341119044002, 0.004740634401053, 0.002970324602745,
           0.001083452606133,-0.000865630072489,-0.002815954068141,-0.004709988255011,
           -0.00648793755008,-0.008093632733016, -0.00947614139746, -0.01058928661538,
           -0.01139612117982, -0.01186845578678, -0.01198641245946, -0.01174244749852,
           -0.01113996734232, -0.01019224266562,-0.008924614848933,-0.007372838773116,
          -0.005580389346067,-0.003598932475611,-0.001487737763381,0.0006902940152947,
            0.00287013265531, 0.004984835573338, 0.006968915142197, 0.008761135810098,
            0.01030508672463,  0.01155108060374,   0.0124588462832,  0.01299759740291,
            0.01314813939981,  0.01290277672865,  0.01226555177479,  0.01125315442778,
            0.00989397611874, 0.008225706741181, 0.006297774218285, 0.004166540242365,
           0.001896299540725,-0.0004453007931248,-0.002787717911928,-0.005059370056728,
          -0.007190879545654,-0.009116497531957, -0.01077720028508, -0.01212102088118,
           -0.01310568649255, -0.01369918515898, -0.01388275044756, -0.01364817144704,
           -0.01300227057072, -0.01196143381695, -0.01055769601382,-0.008831155431085,
          -0.006833793020357,-0.004625512220911,-0.002272606219419,0.0001540329291085,
           0.002580482285354, 0.004933926760931,  0.00714197117726, 0.009138081836791,
            0.01086181912024,  0.01226024822975,  0.01329014482689,  0.01392119207855,
            0.01413378409017,  0.01392119207855,  0.01329014482689,  0.01226024822975,
            0.01086181912024, 0.009138081836791,  0.00714197117726, 0.004933926760931,
           0.002580482285354,0.0001540329291085,-0.002272606219419,-0.004625512220911,
          -0.006833793020357,-0.008831155431085, -0.01055769601382, -0.01196143381695,
           -0.01300227057072, -0.01364817144704, -0.01388275044756, -0.01369918515898,
           -0.01310568649255, -0.01212102088118, -0.01077720028508,-0.009116497531957,
          -0.007190879545654,-0.005059370056728,-0.002787717911928,-0.0004453007931248,
           0.001896299540725, 0.004166540242365, 0.006297774218285, 0.008225706741181,
            0.00989397611874,  0.01125315442778,  0.01226555177479,  0.01290277672865,
            0.01314813939981,  0.01299759740291,   0.0124588462832,  0.01155108060374,
            0.01030508672463, 0.008761135810098, 0.006968915142197, 0.004984835573338,
            0.00287013265531,0.0006902940152947,-0.001487737763381,-0.003598932475611,
          -0.005580389346067,-0.007372838773116,-0.008924614848933, -0.01019224266562,
           -0.01113996734232, -0.01174244749852, -0.01198641245946, -0.01186845578678,
           -0.01139612117982, -0.01058928661538, -0.00947614139746,-0.008093632733016,
           -0.00648793755008,-0.004709988255011,-0.002815954068141,-0.000865630072489,
           0.001083452606133, 0.002970324602745, 0.004740634401053, 0.006341119044002,
           0.007727103801067, 0.008859478083449, 0.009708685221558,  0.01025335498467,
            0.01048221668708,  0.01039316110463, 0.009994854329373, 0.009304500158191,
            0.00834817928906, 0.007159377503369, 0.005778074245991,  0.00425047750763,
           0.002624878849009,0.0009524477417658,-0.000716051824604,-0.002329837301135,
          -0.003840811408387,-0.005205731108713,-0.006386847873759,-0.007351615894703,
           -0.00807549544492,-0.008541777407816,-0.008742476624543,-0.008677257685444,
          -0.008353558509567, -0.00778709706701,-0.007001386713209,-0.006024806636539,
          -0.004891183795439, -0.00363966894867,-0.002311153605604,-0.000946575977579,
          0.0004114639355501, 0.001720916238242, 0.002944464348363, 0.004047460383497,
           0.004998194891376, 0.005773119714807, 0.006354237535557, 0.006727503766789,
           0.006889247348798, 0.006839828120587,  0.00658581106705, 0.006143484642276,
           0.005529086007911, 0.004769825651968, 0.003890756631414, 0.002924124545023,
           0.001900506497928,0.0008544673703095,-0.0001821811649135, -0.00117728895739,
          -0.002103235590706,-0.002933281187277,-0.003646254743997,-0.004223219154112,
          -0.004651891368814,-0.004924284598943,-0.005038512037442,-0.004996971589208,
          -0.004806731336767,-0.004479028056615,-0.004030154141029,-0.003478709016795,
          -0.002846244635872,-0.002154468962661,-0.001427322869883,-0.0006892980467495,
           3.56281409242e-05, 0.000726628325763, 0.001363621065206, 0.001929131448636,
           0.002407805327558,  0.00278993040117, 0.003068875099082, 0.003240955475742,
           0.003304466510038, 0.003264601645696, 0.003128042299722, 0.002903331511417,
           0.002601275661071, 0.002238794062014, 0.001829230957443, 0.001387516590351,
           0.000931260091994,0.0004769069734007,3.441822677622e-05,-0.000377356073866,
          -0.0007505706989655,-0.001074666634451,-0.001340029924093,-0.001545470717307,
          -0.001684786058852,-0.001761697915139, -0.00177580494226, -0.00173331050329,
          -0.001638139889512,-0.001499002763076,-0.001323219622679,-0.001121532125451,
           -0.00090267008712,-0.0006774895176468,-0.0004535311557704, -0.00023946892118,
          -4.164997075235e-05, 0.000132926358846,0.0002795527292128,0.0003947927789294,
          0.0004782778448803,0.0005302677176521,0.0005528535252233,0.0005473680166592,
          0.0005175382138493,0.0004679034339563,0.0004050329491426,0.0003343852207291,
          0.0002609987354465,0.0001882781647909,0.0001221954115939, 6.79687441777e-05,
           2.96570145519e-05,7.252263286112e-06,1.900359632057e-06,1.485849328762e-05,
          4.788527761347e-05,9.576956609821e-05,0.0001545616610582,0.0002219559720448,
          0.0002971260953794,0.0003692008908391, 0.000433895456335,0.0004902446405076,
           0.000533330735982,0.0005508307141623,0.0005545141437316,0.0005297459039242,
          0.0004813395343902,0.0004105728745723,0.0003132891349988, 0.000197805710509,
          6.099090400967e-05,-8.837645163121e-05,-0.0002495174875532, -0.00041310638305,
          -0.0005758450874867,-0.0007281061486427,-0.0008659771915571,-0.0009808822233211,
          -0.001070497428543,-0.001128410175202,-0.001154305367947,-0.001144044675244,
          -0.001098295937037,-0.001014439843217,-0.0008949197372924,-0.0007401254899195,
          -0.0005564510353442,-0.0003481488898286,-0.0001247436297867,0.0001083373794174,
          0.0003424222129859,0.0005735877835598,0.0007936022932914,0.0009969465877898,
           0.001172207178198, 0.001313343898011, 0.001412647743263, 0.001471300835506,
           0.001487507128069,  0.00146287770043, 0.001390318091089, 0.001270904150961,
            0.00110657552397,0.0009131752224675,0.0006939874271298,0.0004512351940108,
          0.0001805618164712,-9.146414767987e-05,-0.0003527880363675,-0.0006070280841274,
          -0.0008662573957286,-0.001063339026327,  -0.0012570664366, -0.00140112824179,
           -0.00150214700297, -0.00156258965523,-0.001570248438616,-0.001534694688966,
           -0.00144915473127,-0.001322994659248,-0.001154859561794,-0.0009549289950061,
          -0.0007252727610012,-0.000477042453559,-0.0002145660363467,5.050630305998e-05,
           0.000313244434114,0.0005625427027526,0.0007935326686037,0.0009967845354258,
           0.001168561387411, 0.001301743162031, 0.001394789911033, 0.001443250934962,
           0.001448092730437, 0.001407737418139, 0.001325734073159, 0.001203331032189,
           0.001046163863749,0.0008574960447962,0.0006444141952839,0.0004112228351153,
          0.0001654675875309,-8.878186361588e-05,-0.0003455057027712,-0.0006024397767347,
          -0.0008567426948171,-0.001110921500459,-0.001368316128307,-0.001639963187201,
          -0.001940456517966,-0.002295978177131,-0.002741540006568, -0.00333102711918,
          -0.004137175915421,-0.005265836073165,-0.006861650411119,-0.009129925090438,
            0.01445480961051
        };
    }
}