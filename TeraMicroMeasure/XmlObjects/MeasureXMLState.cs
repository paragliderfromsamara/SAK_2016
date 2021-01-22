using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaMeasure.Utils;
using NormaMeasure.DBControl.Tables;

namespace TeraMicroMeasure.XmlObjects
{
    public class MeasureXMLState : NormaXmlObject
    {
        const string measureParameterType_TagName = "MeasureParameterType";
        const string measureVoltage_TagName = "MeasureVoltage";
        const string measuredCableID_TagName = "MeasuredCableId";
        const string measuredCableLength_TagName = "MeasuredCableLength";
        const string measuredMaterialId_TagName = "MeasuredMaterialID";
        const string temperature_TagName = "Temperature";
        const string beforeMeasureDelay_TagName = "BeforeMeasureDelay";
        const string afterMeasureDelay_TagName = "AfterMeasureDelay";
        const string averagingTimes_TagName = "AveragingTimes";
        const string capturedDeviceTypeId_TagName = "CapturedDeviceTypeId";
        const string capturedDeviceSerial_TagName = "CapturedDeviceSerial";
        uint measured_parameter_type_id = 1;
        /// <summary>
        /// ID измеряемого параметра
        /// Должен соответствовать ID параметра из таблицы MeasuredParameterType
        /// </summary>
        public uint MeasureTypeId
        {
            get
            {
                return measured_parameter_type_id;
            }
            set
            {
                bool f = measured_parameter_type_id != value;
                if (f)
                {
                    measured_parameter_type_id = value;
                    setChangedFlag(f);
                }
            }
        }

        uint measure_voltage = 10;
        /// <summary>
        /// Испытательное напряжение
        /// </summary>
        public uint MeasureVoltage
        {
            get
            {
                return measure_voltage;
            }
            set
            {
                bool f = measure_voltage != value;
                if (f)
                {
                    measure_voltage = value;
                    setChangedFlag(f);
                }
            }
        }

        int measured_cable_id = -1;
        public int MeasuredCableID
        {
            get
            {
                return measured_cable_id;
            }
            set
            {
                bool f = measured_cable_id != value;
                if (f)
                {
                    measured_cable_id = value;
                    setChangedFlag(f);
                }
            }
        }

        uint measured_cable_length = 1000;

        public uint MeasuredCableLength
        {
            get
            {
                return measured_cable_length;
            }
            set
            {
                bool f = measured_cable_length != value;
                if (f)
                {
                    measured_cable_length = value;
                    setChangedFlag(f);
                }
            }
        }

        int measured_material_id = -1;
        public int MeasuredMaterialID
        {
            get
            {
                return measured_material_id;
            }
            set
            {
                bool f = measured_material_id != value;
                if (f)
                {
                    measured_material_id = value;
                    setChangedFlag(f);
                }
            }
        }

        uint temperature = 20;
        public uint Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                bool f = temperature != value;
                if (f)
                {
                    temperature = value;
                    setChangedFlag(f);
                }
            }
        }

        uint before_measure_delay = 0;

        public uint BeforeMeasureDelay
        {
            get
            {
                return before_measure_delay;
            }
            set
            {
                bool f = before_measure_delay != value;
                if (f)
                {
                    before_measure_delay = value;
                    setChangedFlag(f);
                }
            }
        }

        uint after_measure_delay = 0;

        public uint AfterMeasureDelay
        {
            get
            {
                return after_measure_delay;
            }
            set
            {
                bool f = after_measure_delay != value;
                if (f)
                {
                    after_measure_delay = value;
                    setChangedFlag(f);
                }
            }
        }

        uint averaging_times = 0;
        public uint AveragingTimes
        {
            get
            {
                return averaging_times;
            }
            set
            {
                bool f = averaging_times != value;
                if (f)
                {
                    averaging_times = value;
                    setChangedFlag(f);
                }
            }
        }

        int captured_device_type_id = -1;
        /// <summary>
        /// ID типа захваченного устройства
        /// </summary>
        public int CapturedDeviceTypeId
        {
            get
            {
                return captured_device_type_id;
            }
            set
            {
                bool f = captured_device_type_id != value;
                if (f)
                {
                    captured_device_type_id = value;
                    setChangedFlag(f);
                }
            }
        }

        string captured_device_serial = String.Empty;

        /// <summary>
        /// Серийный номер подключенного устройства
        /// </summary>
        public string CapturedDeviceSerial
        {
            get
            {
                return captured_device_serial;
            }
            set
            {
                bool f = captured_device_serial != value;
                if (f)
                {
                    captured_device_serial = value;
                    setChangedFlag(f);
                }
            }
        } 

        public static MeasureXMLState GetDefault()
        {
            MeasureXMLState s = new MeasureXMLState();
            s.MeasureTypeId = MeasuredParameterType.Rleads;
            s.MeasureVoltage = 10;
            return s;
        }

        public MeasureXMLState() : base()
        {

        }

        public MeasureXMLState(string inner_xml) : base(inner_xml)
        {

        }


        protected override void fillXMLDocument()
        {
            base.fillXMLDocument();
            setXmlProp(measureParameterType_TagName, measured_parameter_type_id.ToString());
            setXmlProp(measureVoltage_TagName, measure_voltage.ToString());
            setXmlProp(measuredCableID_TagName, measured_cable_id.ToString());
            setXmlProp(measuredCableLength_TagName, measured_cable_length.ToString());
            setXmlProp(measuredMaterialId_TagName, measured_material_id.ToString());
            setXmlProp(temperature_TagName, temperature.ToString());
            setXmlProp(beforeMeasureDelay_TagName, before_measure_delay.ToString());
            setXmlProp(afterMeasureDelay_TagName, after_measure_delay.ToString());
            setXmlProp(averagingTimes_TagName, averaging_times.ToString());
            setXmlProp(capturedDeviceTypeId_TagName, captured_device_type_id.ToString());
            setXmlProp(capturedDeviceSerial_TagName, captured_device_serial);
        }

        protected override void buildFromXML()
        {
            fillMeasureParameterTypeFromXML();
            fillMeasureVoltageFromXML();
            fillMeasuredCableIDFromXML();
            fillMeasuredCableLengthFromXML();
            fillMeasuredMaterialIDFormXML();
            fillTemperatureFromXML();
            fillBeforeMeasureDelayFromXML();
            fillAfterMeasureDelayFromXML();
            fillAveragingTimesFromXML();
            fillCapturedDeviceTypeId();
            fillCapturedDeviceSerial();
        }

        private void fillCapturedDeviceSerial()
        {
            captured_device_serial = getXmlProp(capturedDeviceSerial_TagName);
        }

        private void fillCapturedDeviceTypeId()
        {
            int v = -1;
            tryGetIntXmlProp(capturedDeviceTypeId_TagName, out v);
            captured_device_type_id = v;
        }

        private void fillAveragingTimesFromXML()
        {
            uint v = 0;
            tryGetUIntXmlProp(averagingTimes_TagName, out v);
            averaging_times = v;
        }

        private void fillAfterMeasureDelayFromXML()
        {
            uint v = 0;
            tryGetUIntXmlProp(afterMeasureDelay_TagName, out v);
            after_measure_delay = v;
        }

        private void fillBeforeMeasureDelayFromXML()
        {
            uint v = 0;
            tryGetUIntXmlProp(beforeMeasureDelay_TagName, out v);
            before_measure_delay = v;
        }

        private void fillTemperatureFromXML()
        {
            uint v = 20;
            tryGetUIntXmlProp(temperature_TagName, out v);
            if (v < 5) v = 20;
            temperature = v;
        }

        private void fillMeasuredMaterialIDFormXML()
        {
            int v = -1;
            tryGetIntXmlProp(measuredMaterialId_TagName, out v);
            measured_material_id = v;
        }

        private void fillMeasuredCableLengthFromXML()
        {
            uint v = 1;
            tryGetUIntXmlProp(measuredCableLength_TagName, out v);
            if (v == 0) v = 1;
            measured_cable_length = v;
        }

        private void fillMeasuredCableIDFromXML()
        {
            int v = -1;
            tryGetIntXmlProp(measuredCableID_TagName, out v);
            measured_cable_id = v;
        }

        private void fillMeasureVoltageFromXML()
        {
            uint v = 10;
            tryGetUIntXmlProp(measureVoltage_TagName, out v);
            if (v == 0) v = 10;
            measure_voltage = v;
        }

        private void fillMeasureParameterTypeFromXML()
        {
            uint v = 1;
            tryGetUIntXmlProp(measureParameterType_TagName, out v);
            if (v == 0) v = 1;
            measured_parameter_type_id = v;
        }
    }
}
