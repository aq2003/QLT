// SBRF
log("script_started");
v_start_time = 00:00_10.11.14;
v_end_time = 23:30_28.11.14;
v_sar_init = 0.005p;
v_sar_step = 0.005p;
v_sar_max = 0.2p;
v_sar_lmult = 3n;
v_sar_smult = 3n;
fastMA = 48c;
slowMA = (fastMA * 2n);
signalMA = 15c;
v_long_count = 4i;
v_short_count = 4i;
v_i1 = 0i;
v_i2 = 0i;
longTP.period = 120c;
shortTP.period = 120c;
longTP.k_start = 5n;
longTP.k_end = 1n;
shortTP.k_start = 5n;
shortTP.k_end = 1n;
log("variables_initialized");


..[time < v_end_time]
{	
	long(2l) << time >= v_start_time & high #^ ind("sar", v_sar_max, v_sar_step, v_sar_init) /*& ind("macd", "macd", fastMA, slowMA, signalMA) > ind("macd", "signal", fastMA, slowMA, signalMA)*/; 

	log("sar_has_reversed_down");

	v_i1 = 1i;

    ~;

	..[v_i1 < v_long_count & low > ind("sar", v_sar_max, v_sar_step, v_sar_init) & high < pos.price + ind("atr", 24c) * v_sar_lmult = (longTP.k_start * (1c - pos.age/longTP.period))]
	{
		long(2l) << low < ind("pricechannel", "low", 12c) & ind("macd", "macd", fastMA, slowMA, signalMA) > ind("macd", "signal", fastMA, slowMA, signalMA); 
		v_i1 += 1i; 
		log("v_i1_=;" + v_i1); 
		~
	};

	stop() << high > pos.price + ind("atr", 24c) * v_sar_lmult = (longTP.k_start * (1c - pos.age/longTP.period)) | low #_ ind("sar", v_sar_max, v_sar_step, v_sar_init);
 
	log("v_sar_lmult_=;" + v_sar_lmult)
 
|| 

	short(2l) << time >= v_start_time & low #_ ind("sar", v_sar_max, v_sar_step, v_sar_init) /*& ind("macd", "macd", fastMA, slowMA, signalMA) < ind("macd", "signal", fastMA, slowMA, signalMA)*/; 
		
	log("sar_has_reversed_up");

	v_i2 = 1i;

       ~;

	..[v_i2 < v_short_count & high < ind("sar", v_sar_max, v_sar_step, v_sar_init) & low > pos.price - ind("atr", 24c) * v_sar_smult = (shortTP.k_start * (1c - pos.age/shortTP.period))]
	{
		short(2l) << high > ind("pricechannel", "high", 12c) & ind("macd", "macd", fastMA, slowMA, signalMA) < ind("macd", "signal", fastMA, slowMA, signalMA); 
		v_i2 += 1i; 
		log("v_i2_=;" + v_i2); 
		~
	};

	stop() << low < pos.price - ind("atr", 24c) * v_sar_smult = (shortTP.k_start * (1c - pos.age/shortTP.period)) | high #^ ind("sar", v_sar_max, v_sar_step, v_sar_init);
 
	log("v_sar_smult_=;" + v_sar_smult)
};

stop()
