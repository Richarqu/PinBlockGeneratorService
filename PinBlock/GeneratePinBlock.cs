using System;
using org.jpos.iso;
using ThalesSim.Core;
using ThalesSim.Core.Cryptography;

namespace SterlISOsrc
{
    class Utils
    {
		//Set the Key Components
        string comp1 = "F1E54546EF3715DF512510E3EA209179"; //TMK1
        string comp2 = "D3D5B57A64C132BA5189FB685273543B"; //TMK2
        string zpk = "6A808F612631ECC7506E13994C5B832E"; // Working Key
		
		
        //Get padded PAN to form ISO 0 PINBlock format
        public string padPan(string pan)
        {
            string paddedPan = "";
            string r12pan = pan.Substring((pan.Length - 13), 12);

            paddedPan = r12pan.PadLeft(16, '0');
            return paddedPan;
        }
        //Get padded PIN to form ISO-0 PINBlock format
        public string padPin(string pin)
        {
            string paddedPIN = "";

            string prefix = (pin.Length.ToString()).PadLeft(2, '0');

            string appendPIN = prefix + pin;

            paddedPIN = appendPIN.PadRight(16, 'F');

            return paddedPIN;
        }
        //Get zmk to decrypt zpk
        public string GetZMKfromComponents(string comp1, string comp2)
        {
            string zmk = Utility.XORHexStringsFull(comp1, comp2);

            return zmk;
        }
        //Form ISO-0 / ANSI 98 PIN Block format
        public string ISO0_PINBlock(string pan, string pin)
        {
            string pinBlock = "";

            string paddedPAN = padPan(pan);
            string paddedPIN = padPin(pin);

            pinBlock =Utility.XORHexStringsFull(paddedPIN, paddedPAN);

            return pinBlock;
        }
		//Decrypt Zpk with ZMK formed from ZMK Components
        public string decryptZPK(string zpk, string zmk)
        {
            string decryptedZpk = "";

            HexKey hexZmk = new HexKey(zmk);
            decryptedZpk = TripleDES.TripleDESDecrypt(hexZmk, zpk);

            return decryptedZpk;
        }
        //Encrypt PIN Block
        public string encryptPINBlock(string pinBlock, string key)
        {
            string encryptedPINBlock = "";

            HexKey hKey = new HexKey(key);
            encryptedPINBlock = TripleDES.TripleDESEncrypt(hKey, pinBlock);

            return encryptedPINBlock;
        }
        //Encrypt PIN Block with zpk
        public string GetPINdata(string pan, string pin, string zpk, string comp1, string comp2)
        {
            string pindata = "";
            //get the clear PIN Block
            string pinBlock = ISO0_PINBlock(pan, pin);
            //get zmk from the clear ZMK components
            string zmk = GetZMKfromComponents(comp1, comp2);
            //Decrypt the encrypted zpk with the zmk
            string decryptedZpk = decryptZPK(zpk, zmk);
            //Encrypt the PIN Block with the decrypted ZPK
            pindata = encryptPINBlock(pinBlock, decryptedZpk);

            return pindata;
        }

    }
}
