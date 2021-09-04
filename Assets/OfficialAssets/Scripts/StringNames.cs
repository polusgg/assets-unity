using System;
using System.Reflection;

// Token: 0x0200023C RID: 572
[Obfuscation(Exclude = true)]
public enum StringNames
{
	// Token: 0x04000F27 RID: 3879
	ExitButton,
	// Token: 0x04000F28 RID: 3880
	BackButton,
	// Token: 0x04000F29 RID: 3881
	AvailableGamesLabel,
	// Token: 0x04000F2A RID: 3882
	CreateGameButton,
	// Token: 0x04000F2B RID: 3883
	FindGameButton,
	// Token: 0x04000F2C RID: 3884
	EnterCode,
	// Token: 0x04000F2D RID: 3885
	GhostIgnoreTasks,
	// Token: 0x04000F2E RID: 3886
	GhostDoTasks,
	// Token: 0x04000F2F RID: 3887
	GhostImpostor,
	// Token: 0x04000F30 RID: 3888
	ImpostorTask,
	// Token: 0x04000F31 RID: 3889
	FakeTasks,
	// Token: 0x04000F32 RID: 3890
	TaskComplete,
	// Token: 0x04000F33 RID: 3891
	ExileTextSP,
	// Token: 0x04000F34 RID: 3892
	ExileTextSN,
	// Token: 0x04000F35 RID: 3893
	ExileTextPP,
	// Token: 0x04000F36 RID: 3894
	ExileTextPN,
	// Token: 0x04000F37 RID: 3895
	NoExileSkip,
	// Token: 0x04000F38 RID: 3896
	NoExileTie,
	// Token: 0x04000F39 RID: 3897
	ImpostorsRemainS,
	// Token: 0x04000F3A RID: 3898
	ImpostorsRemainP,
	// Token: 0x04000F3B RID: 3899
	Hallway,
	// Token: 0x04000F3C RID: 3900
	Storage,
	// Token: 0x04000F3D RID: 3901
	Cafeteria,
	// Token: 0x04000F3E RID: 3902
	Reactor,
	// Token: 0x04000F3F RID: 3903
	UpperEngine,
	// Token: 0x04000F40 RID: 3904
	Nav,
	// Token: 0x04000F41 RID: 3905
	Admin,
	// Token: 0x04000F42 RID: 3906
	Electrical,
	// Token: 0x04000F43 RID: 3907
	LifeSupp,
	// Token: 0x04000F44 RID: 3908
	Shields,
	// Token: 0x04000F45 RID: 3909
	MedBay,
	// Token: 0x04000F46 RID: 3910
	Security,
	// Token: 0x04000F47 RID: 3911
	Weapons,
	// Token: 0x04000F48 RID: 3912
	LowerEngine,
	// Token: 0x04000F49 RID: 3913
	Comms,
	// Token: 0x04000F4A RID: 3914
	Decontamination,
	// Token: 0x04000F4B RID: 3915
	Launchpad,
	// Token: 0x04000F4C RID: 3916
	LockerRoom,
	// Token: 0x04000F4D RID: 3917
	Laboratory,
	// Token: 0x04000F4E RID: 3918
	Balcony,
	// Token: 0x04000F4F RID: 3919
	Office,
	// Token: 0x04000F50 RID: 3920
	Greenhouse,
	// Token: 0x04000F51 RID: 3921
	DivertPowerTo,
	// Token: 0x04000F52 RID: 3922
	AcceptDivertedPower,
	// Token: 0x04000F53 RID: 3923
	SubmitScan,
	// Token: 0x04000F54 RID: 3924
	PrimeShields,
	// Token: 0x04000F55 RID: 3925
	FuelEngines,
	// Token: 0x04000F56 RID: 3926
	ChartCourse,
	// Token: 0x04000F57 RID: 3927
	StartReactor,
	// Token: 0x04000F58 RID: 3928
	SwipeCard,
	// Token: 0x04000F59 RID: 3929
	ClearAsteroids,
	// Token: 0x04000F5A RID: 3930
	UploadData,
	// Token: 0x04000F5B RID: 3931
	DownloadData,
	// Token: 0x04000F5C RID: 3932
	InspectSample,
	// Token: 0x04000F5D RID: 3933
	EmptyChute,
	// Token: 0x04000F5E RID: 3934
	EmptyGarbage,
	// Token: 0x04000F5F RID: 3935
	AlignEngineOutput,
	// Token: 0x04000F60 RID: 3936
	FixWiring,
	// Token: 0x04000F61 RID: 3937
	CalibrateDistributor,
	// Token: 0x04000F62 RID: 3938
	UnlockManifolds,
	// Token: 0x04000F63 RID: 3939
	ResetReactor,
	// Token: 0x04000F64 RID: 3940
	FixLights,
	// Token: 0x04000F65 RID: 3941
	FixComms,
	// Token: 0x04000F66 RID: 3942
	RestoreOxy,
	// Token: 0x04000F67 RID: 3943
	CleanO2Filter,
	// Token: 0x04000F68 RID: 3944
	StabilizeSteering,
	// Token: 0x04000F69 RID: 3945
	AssembleArtifact,
	// Token: 0x04000F6A RID: 3946
	SortSamples,
	// Token: 0x04000F6B RID: 3947
	MeasureWeather,
	// Token: 0x04000F6C RID: 3948
	EnterIdCode,
	// Token: 0x04000F6D RID: 3949
	HowToPlayText1,
	// Token: 0x04000F6E RID: 3950
	HowToPlayText2,
	// Token: 0x04000F6F RID: 3951
	HowToPlayText5,
	// Token: 0x04000F70 RID: 3952
	HowToPlayText6,
	// Token: 0x04000F71 RID: 3953
	HowToPlayText7,
	// Token: 0x04000F72 RID: 3954
	HowToPlayText81,
	// Token: 0x04000F73 RID: 3955
	HowToPlayText82,
	// Token: 0x04000F74 RID: 3956
	NumImpostorsS,
	// Token: 0x04000F75 RID: 3957
	NumImpostorsP,
	// Token: 0x04000F76 RID: 3958
	Crewmate,
	// Token: 0x04000F77 RID: 3959
	Impostor,
	// Token: 0x04000F78 RID: 3960
	Victory,
	// Token: 0x04000F79 RID: 3961
	Defeat,
	// Token: 0x04000F7A RID: 3962
	CrewmatesDisconnected,
	// Token: 0x04000F7B RID: 3963
	ImpostorDisconnected,
	// Token: 0x04000F7C RID: 3964
	HowToPlayText41,
	// Token: 0x04000F7D RID: 3965
	HowToPlayText42,
	// Token: 0x04000F7E RID: 3966
	HowToPlayText43,
	// Token: 0x04000F7F RID: 3967
	HowToPlayText44,
	// Token: 0x04000F80 RID: 3968
	HowToPlayTextMap,
	// Token: 0x04000F81 RID: 3969
	HowToPlayTextCrew1,
	// Token: 0x04000F82 RID: 3970
	HowToPlayTextCrew2,
	// Token: 0x04000F83 RID: 3971
	HowToPlayTextCrew3,
	// Token: 0x04000F84 RID: 3972
	HowToPlayTextCrew4,
	// Token: 0x04000F85 RID: 3973
	HowToPlayTextCrew5,
	// Token: 0x04000F86 RID: 3974
	HowToPlayTextCrew6,
	// Token: 0x04000F87 RID: 3975
	HowToPlayTextImp1,
	// Token: 0x04000F88 RID: 3976
	HowToPlayTextImp2,
	// Token: 0x04000F89 RID: 3977
	HowToPlayTextImp3,
	// Token: 0x04000F8A RID: 3978
	HowToPlayTextImp4,
	// Token: 0x04000F8B RID: 3979
	HowToPlayTextImp5,
	// Token: 0x04000F8C RID: 3980
	HowToPlayTextImp6,
	// Token: 0x04000F8D RID: 3981
	HowToPlayTextImp7,
	// Token: 0x04000F8E RID: 3982
	SettingsGeneral,
	// Token: 0x04000F8F RID: 3983
	SettingsControls,
	// Token: 0x04000F90 RID: 3984
	SettingsSound,
	// Token: 0x04000F91 RID: 3985
	SettingsGraphics,
	// Token: 0x04000F92 RID: 3986
	SettingsData,
	// Token: 0x04000F93 RID: 3987
	SettingsCensorChat,
	// Token: 0x04000F94 RID: 3988
	SettingsMusic,
	// Token: 0x04000F95 RID: 3989
	SettingsSFX,
	// Token: 0x04000F96 RID: 3990
	SettingsOn,
	// Token: 0x04000F97 RID: 3991
	SettingsOff,
	// Token: 0x04000F98 RID: 3992
	SettingsSendTelemetry,
	// Token: 0x04000F99 RID: 3993
	SettingsControlMode,
	// Token: 0x04000F9A RID: 3994
	SettingsTouchMode,
	// Token: 0x04000F9B RID: 3995
	SettingsJoystickMode,
	// Token: 0x04000F9C RID: 3996
	SettingsKeyboardMode,
	// Token: 0x04000F9D RID: 3997
	SettingsFullscreen,
	// Token: 0x04000F9E RID: 3998
	SettingsResolution,
	// Token: 0x04000F9F RID: 3999
	SettingsApply,
	// Token: 0x04000FA0 RID: 4000
	SettingsPersonalizeAds,
	// Token: 0x04000FA1 RID: 4001
	SettingsLanguage,
	// Token: 0x04000FA2 RID: 4002
	SettingsJoystickSize,
	// Token: 0x04000FA3 RID: 4003
	SettingsMouseMode,
	// Token: 0x04000FA4 RID: 4004
	PlayerColor,
	// Token: 0x04000FA5 RID: 4005
	PlayerHat,
	// Token: 0x04000FA6 RID: 4006
	PlayerSkin,
	// Token: 0x04000FA7 RID: 4007
	PlayerPet,
	// Token: 0x04000FA8 RID: 4008
	GameSettings,
	// Token: 0x04000FA9 RID: 4009
	GameRecommendedSettings,
	// Token: 0x04000FAA RID: 4010
	GameCustomSettings,
	// Token: 0x04000FAB RID: 4011
	GameMapName,
	// Token: 0x04000FAC RID: 4012
	GameNumImpostors,
	// Token: 0x04000FAD RID: 4013
	GameNumMeetings,
	// Token: 0x04000FAE RID: 4014
	GameDiscussTime,
	// Token: 0x04000FAF RID: 4015
	GameVotingTime,
	// Token: 0x04000FB0 RID: 4016
	GamePlayerSpeed,
	// Token: 0x04000FB1 RID: 4017
	GameCrewLight,
	// Token: 0x04000FB2 RID: 4018
	GameImpostorLight,
	// Token: 0x04000FB3 RID: 4019
	GameKillCooldown,
	// Token: 0x04000FB4 RID: 4020
	GameKillDistance,
	// Token: 0x04000FB5 RID: 4021
	GameCommonTasks,
	// Token: 0x04000FB6 RID: 4022
	GameLongTasks,
	// Token: 0x04000FB7 RID: 4023
	GameShortTasks,
	// Token: 0x04000FB8 RID: 4024
	MatchMapName,
	// Token: 0x04000FB9 RID: 4025
	MatchLanguage,
	// Token: 0x04000FBA RID: 4026
	MatchImpostors,
	// Token: 0x04000FBB RID: 4027
	MatchMaxPlayers,
	// Token: 0x04000FBC RID: 4028
	Cancel,
	// Token: 0x04000FBD RID: 4029
	Confirm,
	// Token: 0x04000FBE RID: 4030
	Limit,
	// Token: 0x04000FBF RID: 4031
	RoomCode,
	// Token: 0x04000FC0 RID: 4032
	LeaveGame,
	// Token: 0x04000FC1 RID: 4033
	ReturnToGame,
	// Token: 0x04000FC2 RID: 4034
	LocalHelp,
	// Token: 0x04000FC3 RID: 4035
	OnlineHelp,
	// Token: 0x04000FC4 RID: 4036
	SettingsVSync,
	// Token: 0x04000FC5 RID: 4037
	EmergencyCount,
	// Token: 0x04000FC6 RID: 4038
	EmergencyNotReady,
	// Token: 0x04000FC7 RID: 4039
	EmergencyDuringCrisis,
	// Token: 0x04000FC8 RID: 4040
	EmergencyRequested,
	// Token: 0x04000FC9 RID: 4041
	GameEmergencyCooldown,
	// Token: 0x04000FCA RID: 4042
	BuyBeverage,
	// Token: 0x04000FCB RID: 4043
	WeatherEta,
	// Token: 0x04000FCC RID: 4044
	WeatherComplete,
	// Token: 0x04000FCD RID: 4045
	ProcessData,
	// Token: 0x04000FCE RID: 4046
	RunDiagnostics,
	// Token: 0x04000FCF RID: 4047
	WaterPlants,
	// Token: 0x04000FD0 RID: 4048
	PickAnomaly,
	// Token: 0x04000FD1 RID: 4049
	WaterPlantsGetCan,
	// Token: 0x04000FD2 RID: 4050
	AuthOfficeOkay,
	// Token: 0x04000FD3 RID: 4051
	AuthCommsOkay,
	// Token: 0x04000FD4 RID: 4052
	AuthOfficeActive,
	// Token: 0x04000FD5 RID: 4053
	AuthCommsActive,
	// Token: 0x04000FD6 RID: 4054
	AuthOfficeNotActive,
	// Token: 0x04000FD7 RID: 4055
	AuthCommsNotActive,
	// Token: 0x04000FD8 RID: 4056
	SecLogEntry,
	// Token: 0x04000FD9 RID: 4057
	EnterName,
	// Token: 0x04000FDA RID: 4058
	SwipeCardPleaseSwipe,
	// Token: 0x04000FDB RID: 4059
	SwipeCardBadRead,
	// Token: 0x04000FDC RID: 4060
	SwipeCardTooFast,
	// Token: 0x04000FDD RID: 4061
	SwipeCardTooSlow,
	// Token: 0x04000FDE RID: 4062
	SwipeCardAccepted,
	// Token: 0x04000FDF RID: 4063
	ReactorHoldToStop,
	// Token: 0x04000FE0 RID: 4064
	ReactorWaiting,
	// Token: 0x04000FE1 RID: 4065
	ReactorNominal,
	// Token: 0x04000FE2 RID: 4066
	MeetingWhoIsTitle,
	// Token: 0x04000FE3 RID: 4067
	MeetingVotingBegins,
	// Token: 0x04000FE4 RID: 4068
	MeetingVotingEnds,
	// Token: 0x04000FE5 RID: 4069
	MeetingVotingResults,
	// Token: 0x04000FE6 RID: 4070
	MeetingProceeds,
	// Token: 0x04000FE7 RID: 4071
	MeetingHasVoted,
	// Token: 0x04000FE8 RID: 4072
	DataPolicyTitle,
	// Token: 0x04000FE9 RID: 4073
	DataPolicyText,
	// Token: 0x04000FEA RID: 4074
	DataPolicyWhat,
	// Token: 0x04000FEB RID: 4075
	AdPolicyTitle,
	// Token: 0x04000FEC RID: 4076
	AdPolicyText,
	// Token: 0x04000FED RID: 4077
	Accept,
	// Token: 0x04000FEE RID: 4078
	RemoveAds,
	// Token: 0x04000FEF RID: 4079
	SwipeCardPleaseInsert,
	// Token: 0x04000FF0 RID: 4080
	LogNorth,
	// Token: 0x04000FF1 RID: 4081
	LogSouthEast,
	// Token: 0x04000FF2 RID: 4082
	LogSouthWest,
	// Token: 0x04000FF3 RID: 4083
	SettingShort,
	// Token: 0x04000FF4 RID: 4084
	SettingMedium,
	// Token: 0x04000FF5 RID: 4085
	SettingLong,
	// Token: 0x04000FF6 RID: 4086
	SamplesPress,
	// Token: 0x04000FF7 RID: 4087
	SamplesAdding,
	// Token: 0x04000FF8 RID: 4088
	SamplesSelect,
	// Token: 0x04000FF9 RID: 4089
	SamplesThanks,
	// Token: 0x04000FFA RID: 4090
	SamplesComplete,
	// Token: 0x04000FFB RID: 4091
	AstDestroyed,
	// Token: 0x04000FFC RID: 4092
	TaskTestTitle,
	// Token: 0x04000FFD RID: 4093
	BeginDiagnostics,
	// Token: 0x04000FFE RID: 4094
	UserLeftGame,
	// Token: 0x04000FFF RID: 4095
	GameStarting,
	// Token: 0x04001000 RID: 4096
	Tasks,
	// Token: 0x04001001 RID: 4097
	StatsTitle,
	// Token: 0x04001002 RID: 4098
	StatsBodiesReported,
	// Token: 0x04001003 RID: 4099
	StatsEmergenciesCalled,
	// Token: 0x04001004 RID: 4100
	StatsTasksCompleted,
	// Token: 0x04001005 RID: 4101
	StatsAllTasksCompleted,
	// Token: 0x04001006 RID: 4102
	StatsSabotagesFixed,
	// Token: 0x04001007 RID: 4103
	StatsImpostorKills,
	// Token: 0x04001008 RID: 4104
	StatsTimesMurdered,
	// Token: 0x04001009 RID: 4105
	StatsTimesEjected,
	// Token: 0x0400100A RID: 4106
	StatsCrewmateStreak,
	// Token: 0x0400100B RID: 4107
	StatsGamesImpostor,
	// Token: 0x0400100C RID: 4108
	StatsGamesCrewmate,
	// Token: 0x0400100D RID: 4109
	StatsGamesStarted,
	// Token: 0x0400100E RID: 4110
	StatsGamesFinished,
	// Token: 0x0400100F RID: 4111
	StatsImpostorVoteWins,
	// Token: 0x04001010 RID: 4112
	StatsImpostorKillsWins,
	// Token: 0x04001011 RID: 4113
	StatsImpostorSabotageWins,
	// Token: 0x04001012 RID: 4114
	StatsCrewmateVoteWins,
	// Token: 0x04001013 RID: 4115
	StatsCrewmateTaskWins,
	// Token: 0x04001014 RID: 4116
	MedscanRequested,
	// Token: 0x04001015 RID: 4117
	MedscanWaitingFor,
	// Token: 0x04001016 RID: 4118
	MedscanCompleted,
	// Token: 0x04001017 RID: 4119
	MedscanCompleteIn,
	// Token: 0x04001018 RID: 4120
	MonitorOxygen,
	// Token: 0x04001019 RID: 4121
	StoreArtifacts,
	// Token: 0x0400101A RID: 4122
	FillCanisters,
	// Token: 0x0400101B RID: 4123
	FixWeatherNode,
	// Token: 0x0400101C RID: 4124
	InsertKeys,
	// Token: 0x0400101D RID: 4125
	ResetSeismic,
	// Token: 0x0400101E RID: 4126
	SeismicHoldToStop,
	// Token: 0x0400101F RID: 4127
	SeismicNominal,
	// Token: 0x04001020 RID: 4128
	ScanBoardingPass,
	// Token: 0x04001021 RID: 4129
	OpenWaterways,
	// Token: 0x04001022 RID: 4130
	ReplaceWaterJug,
	// Token: 0x04001023 RID: 4131
	RepairDrill,
	// Token: 0x04001024 RID: 4132
	AlignTelescope,
	// Token: 0x04001025 RID: 4133
	RecordTemperature,
	// Token: 0x04001026 RID: 4134
	RebootWifi,
	// Token: 0x04001027 RID: 4135
	WifiRebootRequired,
	// Token: 0x04001028 RID: 4136
	WifiPleasePowerOn,
	// Token: 0x04001029 RID: 4137
	WifiPleaseWait,
	// Token: 0x0400102A RID: 4138
	WifiPleaseReturnIn,
	// Token: 0x0400102B RID: 4139
	WifiRebootComplete,
	// Token: 0x0400102C RID: 4140
	Outside,
	// Token: 0x0400102D RID: 4141
	GameSecondsAbbrev,
	// Token: 0x0400102E RID: 4142
	Engines,
	// Token: 0x0400102F RID: 4143
	Dropship,
	// Token: 0x04001030 RID: 4144
	Decontamination2,
	// Token: 0x04001031 RID: 4145
	Specimens,
	// Token: 0x04001032 RID: 4146
	BoilerRoom,
	// Token: 0x04001033 RID: 4147
	GameOverImpostorDead,
	// Token: 0x04001034 RID: 4148
	GameOverImpostorKills,
	// Token: 0x04001035 RID: 4149
	GameOverTaskWin,
	// Token: 0x04001036 RID: 4150
	GameOverSabotage,
	// Token: 0x04001037 RID: 4151
	GameConfirmImpostor,
	// Token: 0x04001038 RID: 4152
	GameVisualTasks,
	// Token: 0x04001039 RID: 4153
	ExileTextNonConfirm,
	// Token: 0x0400103A RID: 4154
	GameAnonymousVotes,
	// Token: 0x0400103B RID: 4155
	GameTaskBarMode,
	// Token: 0x0400103C RID: 4156
	SettingNormalTaskMode,
	// Token: 0x0400103D RID: 4157
	SettingMeetingTaskMode,
	// Token: 0x0400103E RID: 4158
	SettingInvisibleTaskMode,
	// Token: 0x0400103F RID: 4159
	PlainYes,
	// Token: 0x04001040 RID: 4160
	PlainNo,
	// Token: 0x04001041 RID: 4161
	PrivacyPolicyTitle,
	// Token: 0x04001042 RID: 4162
	PrivacyPolicyText,
	// Token: 0x04001043 RID: 4163
	ManageDataButton,
	// Token: 0x04001044 RID: 4164
	UnderstandButton,
	// Token: 0x04001045 RID: 4165
	HowToPlayText2Switch,
	// Token: 0x04001046 RID: 4166
	ChatRateLimit,
	// Token: 0x04001047 RID: 4167
	TotalTasksCompleted,
	// Token: 0x04001048 RID: 4168
	ServerNA,
	// Token: 0x04001049 RID: 4169
	ServerEU,
	// Token: 0x0400104A RID: 4170
	ServerAS,
	// Token: 0x0400104B RID: 4171
	ServerSA,
	// Token: 0x0400104C RID: 4172
	LangEnglish,
	// Token: 0x0400104D RID: 4173
	LangFrench,
	// Token: 0x0400104E RID: 4174
	LangItalian,
	// Token: 0x0400104F RID: 4175
	LangGerman,
	// Token: 0x04001050 RID: 4176
	LangSpanish,
	// Token: 0x04001051 RID: 4177
	LangSpanishLATAM,
	// Token: 0x04001052 RID: 4178
	LangBrazPort,
	// Token: 0x04001053 RID: 4179
	LangPort,
	// Token: 0x04001054 RID: 4180
	LangRussian,
	// Token: 0x04001055 RID: 4181
	LangJapanese,
	// Token: 0x04001056 RID: 4182
	LangKorean,
	// Token: 0x04001057 RID: 4183
	LangDutch,
	// Token: 0x04001058 RID: 4184
	LangFilipino,
	// Token: 0x04001059 RID: 4185
	PlayerName,
	// Token: 0x0400105A RID: 4186
	MyTablet,
	// Token: 0x0400105B RID: 4187
	Download,
	// Token: 0x0400105C RID: 4188
	DownloadComplete,
	// Token: 0x0400105D RID: 4189
	DownloadTestEstTimeS,
	// Token: 0x0400105E RID: 4190
	DownloadTestEstTimeMS,
	// Token: 0x0400105F RID: 4191
	DownloadTestEstTimeHMS,
	// Token: 0x04001060 RID: 4192
	DownloadTestEstTimeDHMS,
	// Token: 0x04001061 RID: 4193
	Upload,
	// Token: 0x04001062 RID: 4194
	Headquarters,
	// Token: 0x04001063 RID: 4195
	GrabCoffee,
	// Token: 0x04001064 RID: 4196
	TakeBreak,
	// Token: 0x04001065 RID: 4197
	DontNeedWait,
	// Token: 0x04001066 RID: 4198
	DoSomethingElse,
	// Token: 0x04001067 RID: 4199
	NodeTB,
	// Token: 0x04001068 RID: 4200
	NodeIRO,
	// Token: 0x04001069 RID: 4201
	NodeGI,
	// Token: 0x0400106A RID: 4202
	NodePD,
	// Token: 0x0400106B RID: 4203
	NodeCA,
	// Token: 0x0400106C RID: 4204
	NodeMLG,
	// Token: 0x0400106D RID: 4205
	Vending,
	// Token: 0x0400106E RID: 4206
	OtherLanguage,
	// Token: 0x0400106F RID: 4207
	ImposterAmtAny,
	// Token: 0x04001070 RID: 4208
	VitalsORGN,
	// Token: 0x04001071 RID: 4209
	VitalsBLUE,
	// Token: 0x04001072 RID: 4210
	VitalsRED,
	// Token: 0x04001073 RID: 4211
	VitalsBRWN,
	// Token: 0x04001074 RID: 4212
	VitalsGRN,
	// Token: 0x04001075 RID: 4213
	VitalsPINK,
	// Token: 0x04001076 RID: 4214
	VitalsWHTE,
	// Token: 0x04001077 RID: 4215
	VitalsYLOW,
	// Token: 0x04001078 RID: 4216
	VitalsBLAK,
	// Token: 0x04001079 RID: 4217
	VitalsPURP,
	// Token: 0x0400107A RID: 4218
	VitalsCYAN,
	// Token: 0x0400107B RID: 4219
	VitalsLIME,
	// Token: 0x0400107C RID: 4220
	VitalsOK,
	// Token: 0x0400107D RID: 4221
	VitalsDEAD,
	// Token: 0x0400107E RID: 4222
	VitalsDC,
	// Token: 0x0400107F RID: 4223
	ColorOrange,
	// Token: 0x04001080 RID: 4224
	ColorBlue,
	// Token: 0x04001081 RID: 4225
	ColorRed,
	// Token: 0x04001082 RID: 4226
	ColorBrown,
	// Token: 0x04001083 RID: 4227
	ColorGreen,
	// Token: 0x04001084 RID: 4228
	ColorPink,
	// Token: 0x04001085 RID: 4229
	ColorWhite,
	// Token: 0x04001086 RID: 4230
	ColorYellow,
	// Token: 0x04001087 RID: 4231
	ColorBlack,
	// Token: 0x04001088 RID: 4232
	ColorPurple,
	// Token: 0x04001089 RID: 4233
	ColorCyan,
	// Token: 0x0400108A RID: 4234
	ColorLime,
	// Token: 0x0400108B RID: 4235
	MedID,
	// Token: 0x0400108C RID: 4236
	MedC,
	// Token: 0x0400108D RID: 4237
	MedHT,
	// Token: 0x0400108E RID: 4238
	MedBT,
	// Token: 0x0400108F RID: 4239
	MedWT,
	// Token: 0x04001090 RID: 4240
	MedETA,
	// Token: 0x04001091 RID: 4241
	MedHello,
	// Token: 0x04001092 RID: 4242
	PetFailFetchData,
	// Token: 0x04001093 RID: 4243
	BadResult,
	// Token: 0x04001094 RID: 4244
	More,
	// Token: 0x04001095 RID: 4245
	Processing,
	// Token: 0x04001096 RID: 4246
	ExitGame,
	// Token: 0x04001097 RID: 4247
	WaitingForHost,
	// Token: 0x04001098 RID: 4248
	LeftGameError,
	// Token: 0x04001099 RID: 4249
	PlayerWasBannedBy,
	// Token: 0x0400109A RID: 4250
	PlayerWasKickedBy,
	// Token: 0x0400109B RID: 4251
	CamEast,
	// Token: 0x0400109C RID: 4252
	CamCentral,
	// Token: 0x0400109D RID: 4253
	CamNortheast,
	// Token: 0x0400109E RID: 4254
	CamSouth,
	// Token: 0x0400109F RID: 4255
	CamSouthwest,
	// Token: 0x040010A0 RID: 4256
	CamNorthwest,
	// Token: 0x040010A1 RID: 4257
	LoadingFailed,
	// Token: 0x040010A2 RID: 4258
	LobbySizeWarning,
	// Token: 0x040010A3 RID: 4259
	Okay,
	// Token: 0x040010A4 RID: 4260
	OkayDontShow,
	// Token: 0x040010A5 RID: 4261
	Nevermind,
	// Token: 0x040010A6 RID: 4262
	Dummy,
	// Token: 0x040010A7 RID: 4263
	Bad,
	// Token: 0x040010A8 RID: 4264
	Status,
	// Token: 0x040010A9 RID: 4265
	Fine,
	// Token: 0x040010AA RID: 4266
	OK,
	// Token: 0x040010AB RID: 4267
	PetTryOn,
	// Token: 0x040010AC RID: 4268
	SecondsAbbv,
	// Token: 0x040010AD RID: 4269
	SecurityLogsSystem,
	// Token: 0x040010AE RID: 4270
	SecurityCamsSystem,
	// Token: 0x040010AF RID: 4271
	AdminMapSystem,
	// Token: 0x040010B0 RID: 4272
	VitalsSystem,
	// Token: 0x040010B1 RID: 4273
	BanButton,
	// Token: 0x040010B2 RID: 4274
	KickButton,
	// Token: 0x040010B3 RID: 4275
	ReportButton,
	// Token: 0x040010B4 RID: 4276
	ReportConfirmation,
	// Token: 0x040010B5 RID: 4277
	ReportBadName,
	// Token: 0x040010B6 RID: 4278
	ReportBadChat,
	// Token: 0x040010B7 RID: 4279
	ReportHacking,
	// Token: 0x040010B8 RID: 4280
	ReportHarassment,
	// Token: 0x040010B9 RID: 4281
	ReportWhy,
	// Token: 0x040010BA RID: 4282
	ErrorServerOverload = 700,
	// Token: 0x040010BB RID: 4283
	ErrorIntentionalLeaving,
	// Token: 0x040010BC RID: 4284
	ErrorFocusLost,
	// Token: 0x040010BD RID: 4285
	ErrorBanned,
	// Token: 0x040010BE RID: 4286
	ErrorKicked,
	// Token: 0x040010BF RID: 4287
	ErrorBannedNoCode,
	// Token: 0x040010C0 RID: 4288
	ErrorKickedNoCode,
	// Token: 0x040010C1 RID: 4289
	ErrorHacking,
	// Token: 0x040010C2 RID: 4290
	ErrorFullGame,
	// Token: 0x040010C3 RID: 4291
	ErrorStartedGame,
	// Token: 0x040010C4 RID: 4292
	ErrorNotFoundGame,
	// Token: 0x040010C5 RID: 4293
	ErrorInactivity,
	// Token: 0x040010C6 RID: 4294
	ErrorGenericOnlineDisconnect,
	// Token: 0x040010C7 RID: 4295
	ErrorGenericLocalDisconnect,
	// Token: 0x040010C8 RID: 4296
	ErrorInvalidName,
	// Token: 0x040010C9 RID: 4297
	ErrorUnknown,
	// Token: 0x040010CA RID: 4298
	ErrorIncorrectVersion,
	// Token: 0x040010CB RID: 4299
	ErrorNotAuthenticated,
	// Token: 0x040010CC RID: 4300
	VentDirection = 1000,
	// Token: 0x040010CD RID: 4301
	VentMove,
	// Token: 0x040010CE RID: 4302
	MenuNavigate,
	// Token: 0x040010CF RID: 4303
	NoTranslation,
	// Token: 0x040010D0 RID: 4304
	NsoError,
	// Token: 0x040010D1 RID: 4305
	PolishRuby = 500,
	// Token: 0x040010D2 RID: 4306
	ResetBreakers,
	// Token: 0x040010D3 RID: 4307
	Decontaminate,
	// Token: 0x040010D4 RID: 4308
	MakeBurger,
	// Token: 0x040010D5 RID: 4309
	UnlockSafe,
	// Token: 0x040010D6 RID: 4310
	SortRecords,
	// Token: 0x040010D7 RID: 4311
	PutAwayPistols,
	// Token: 0x040010D8 RID: 4312
	FixShower,
	// Token: 0x040010D9 RID: 4313
	CleanToilet,
	// Token: 0x040010DA RID: 4314
	DressMannequin,
	// Token: 0x040010DB RID: 4315
	PickUpTowels,
	// Token: 0x040010DC RID: 4316
	RewindTapes,
	// Token: 0x040010DD RID: 4317
	StartFans,
	// Token: 0x040010DE RID: 4318
	DevelopPhotos,
	// Token: 0x040010DF RID: 4319
	GetBiggolSword,
	// Token: 0x040010E0 RID: 4320
	PutAwayRifles,
	// Token: 0x040010E1 RID: 4321
	StopCharles,
	// Token: 0x040010E2 RID: 4322
	AuthLeftOkay,
	// Token: 0x040010E3 RID: 4323
	AuthRightOkay,
	// Token: 0x040010E4 RID: 4324
	AuthLeftActive,
	// Token: 0x040010E5 RID: 4325
	AuthRightActive,
	// Token: 0x040010E6 RID: 4326
	AuthLeftNotActive,
	// Token: 0x040010E7 RID: 4327
	AuthRightNotActive,
	// Token: 0x040010E8 RID: 4328
	VaultRoom = 550,
	// Token: 0x040010E9 RID: 4329
	Cockpit,
	// Token: 0x040010EA RID: 4330
	Armory,
	// Token: 0x040010EB RID: 4331
	Kitchen,
	// Token: 0x040010EC RID: 4332
	ViewingDeck,
	// Token: 0x040010ED RID: 4333
	HallOfPortraits,
	// Token: 0x040010EE RID: 4334
	Medical,
	// Token: 0x040010EF RID: 4335
	CargoBay,
	// Token: 0x040010F0 RID: 4336
	Ventilation,
	// Token: 0x040010F1 RID: 4337
	Showers,
	// Token: 0x040010F2 RID: 4338
	Engine,
	// Token: 0x040010F3 RID: 4339
	Brig,
	// Token: 0x040010F4 RID: 4340
	MeetingRoom,
	// Token: 0x040010F5 RID: 4341
	Records,
	// Token: 0x040010F6 RID: 4342
	Lounge,
	// Token: 0x040010F7 RID: 4343
	GapRoom,
	// Token: 0x040010F8 RID: 4344
	MainHall,
	// Token: 0x040010F9 RID: 4345
	RevealCode,
	// Token: 0x040010FA RID: 4346
	DirtyHeader,
	// Token: 0x040010FB RID: 4347
	QCLocationLaptop = 2000,
	// Token: 0x040010FC RID: 4348
	QCLocationSkeld,
	// Token: 0x040010FD RID: 4349
	QCLocationMira,
	// Token: 0x040010FE RID: 4350
	QCLocationPolus,
	// Token: 0x040010FF RID: 4351
	QCSystemsStart,
	// Token: 0x04001100 RID: 4352
	QCSystemsKick,
	// Token: 0x04001101 RID: 4353
	QCCrewI,
	// Token: 0x04001102 RID: 4354
	QCCrewMe,
	// Token: 0x04001103 RID: 4355
	QCCrewNoOne,
	// Token: 0x04001104 RID: 4356
	QCAccAKilledB,
	// Token: 0x04001105 RID: 4357
	QCAccAKilledBNeg,
	// Token: 0x04001106 RID: 4358
	QCAccAIsSuspicious,
	// Token: 0x04001107 RID: 4359
	QCAccAIsSuspiciousNeg,
	// Token: 0x04001108 RID: 4360
	QCAccASawBVent,
	// Token: 0x04001109 RID: 4361
	QCAccASawBVentNeg,
	// Token: 0x0400110A RID: 4362
	QCAccAWasChasingB,
	// Token: 0x0400110B RID: 4363
	QCAccAWasChasingBNeg,
	// Token: 0x0400110C RID: 4364
	QCAccAIsLying,
	// Token: 0x0400110D RID: 4365
	QCAccAIsLyingNeg,
	// Token: 0x0400110E RID: 4366
	QCAccVoteA,
	// Token: 0x0400110F RID: 4367
	QCAccVoteANeg,
	// Token: 0x04001110 RID: 4368
	QCAccADidntReport,
	// Token: 0x04001111 RID: 4369
	QCResYes,
	// Token: 0x04001112 RID: 4370
	QCResNo,
	// Token: 0x04001113 RID: 4371
	QCResDontKnow,
	// Token: 0x04001114 RID: 4372
	QCResDontKnowNeg,
	// Token: 0x04001115 RID: 4373
	QCResAWas,
	// Token: 0x04001116 RID: 4374
	QCResAWasNeg,
	// Token: 0x04001117 RID: 4375
	QCResADid,
	// Token: 0x04001118 RID: 4376
	QCResADidNeg,
	// Token: 0x04001119 RID: 4377
	QCResVote,
	// Token: 0x0400111A RID: 4378
	QCResVoteNeg,
	// Token: 0x0400111B RID: 4379
	QCResAWasAtB,
	// Token: 0x0400111C RID: 4380
	QCResAWasAtBNeg,
	// Token: 0x0400111D RID: 4381
	QCResRip,
	// Token: 0x0400111E RID: 4382
	QCResRipNeg,
	// Token: 0x0400111F RID: 4383
	QCResLies,
	// Token: 0x04001120 RID: 4384
	QCResLiesNeg,
	// Token: 0x04001121 RID: 4385
	QCQstWhere,
	// Token: 0x04001122 RID: 4386
	QCQstWho,
	// Token: 0x04001123 RID: 4387
	QCQstWhoWasWith,
	// Token: 0x04001124 RID: 4388
	QCQstWhatWasADoing,
	// Token: 0x04001125 RID: 4389
	QCQstWhoFixedA,
	// Token: 0x04001126 RID: 4390
	QCQstWhereWasA,
	// Token: 0x04001127 RID: 4391
	QCQstBodyOrMeeting,
	// Token: 0x04001128 RID: 4392
	QCStaASawB,
	// Token: 0x04001129 RID: 4393
	QCStaAWasWithB,
	// Token: 0x0400112A RID: 4394
	QCStaADidB,
	// Token: 0x0400112B RID: 4395
	QCStaASelfReported,
	// Token: 0x0400112C RID: 4396
	QCStaDoubleKill,
	// Token: 0x0400112D RID: 4397
	QCStaWasSelfReport,
	// Token: 0x0400112E RID: 4398
	QCStaPleaseDoTasks,
	// Token: 0x0400112F RID: 4399
	QCStaBodyWasInA,
	// Token: 0x04001130 RID: 4400
	QCStaACalledMeeting,
	// Token: 0x04001131 RID: 4401
	QCLocation,
	// Token: 0x04001132 RID: 4402
	QCSystems,
	// Token: 0x04001133 RID: 4403
	QCCrew,
	// Token: 0x04001134 RID: 4404
	QCAccusation,
	// Token: 0x04001135 RID: 4405
	QCResponse,
	// Token: 0x04001136 RID: 4406
	QCQuestion,
	// Token: 0x04001137 RID: 4407
	QCStatements,
	// Token: 0x04001138 RID: 4408
	ANY,
	// Token: 0x04001139 RID: 4409
	ChatType,
	// Token: 0x0400113A RID: 4410
	QuickChatOnly,
	// Token: 0x0400113B RID: 4411
	FreeChatOnly,
	// Token: 0x0400113C RID: 4412
	FreeOrQuickChat,
	// Token: 0x0400113D RID: 4413
	DateOfBirth,
	// Token: 0x0400113E RID: 4414
	DateOfBirthEnter,
	// Token: 0x0400113F RID: 4415
	Month,
	// Token: 0x04001140 RID: 4416
	Day,
	// Token: 0x04001141 RID: 4417
	Year,
	// Token: 0x04001142 RID: 4418
	January,
	// Token: 0x04001143 RID: 4419
	February,
	// Token: 0x04001144 RID: 4420
	March,
	// Token: 0x04001145 RID: 4421
	April,
	// Token: 0x04001146 RID: 4422
	May,
	// Token: 0x04001147 RID: 4423
	June,
	// Token: 0x04001148 RID: 4424
	July,
	// Token: 0x04001149 RID: 4425
	August,
	// Token: 0x0400114A RID: 4426
	September,
	// Token: 0x0400114B RID: 4427
	October,
	// Token: 0x0400114C RID: 4428
	November,
	// Token: 0x0400114D RID: 4429
	December,
	// Token: 0x0400114E RID: 4430
	Submit,
	// Token: 0x0400114F RID: 4431
	QCMore,
	// Token: 0x04001150 RID: 4432
	Success,
	// Token: 0x04001151 RID: 4433
	Failed,
	// Token: 0x04001152 RID: 4434
	ErrorCreate,
	// Token: 0x04001153 RID: 4435
	SuccessCreate,
	// Token: 0x04001154 RID: 4436
	Close,
	// Token: 0x04001155 RID: 4437
	ErrorLogIn,
	// Token: 0x04001156 RID: 4438
	SuccessLogIn,
	// Token: 0x04001157 RID: 4439
	AccountInfo,
	// Token: 0x04001158 RID: 4440
	Account,
	// Token: 0x04001159 RID: 4441
	UserName,
	// Token: 0x0400115A RID: 4442
	Height,
	// Token: 0x0400115B RID: 4443
	Weight,
	// Token: 0x0400115C RID: 4444
	SignIn,
	// Token: 0x0400115D RID: 4445
	CreateAccount,
	// Token: 0x0400115E RID: 4446
	RequestPermission,
	// Token: 0x0400115F RID: 4447
	RandomizeName,
	// Token: 0x04001160 RID: 4448
	AccountLinking,
	// Token: 0x04001161 RID: 4449
	ChangeName,
	// Token: 0x04001162 RID: 4450
	LogOut,
	// Token: 0x04001163 RID: 4451
	GuardianWait,
	// Token: 0x04001164 RID: 4452
	EmailEdit,
	// Token: 0x04001165 RID: 4453
	EmailResend,
	// Token: 0x04001166 RID: 4454
	GuestContinue,
	// Token: 0x04001167 RID: 4455
	GuardianEmailSent,
	// Token: 0x04001168 RID: 4456
	GuardianCheckEmail,
	// Token: 0x04001169 RID: 4457
	EditName,
	// Token: 0x0400116A RID: 4458
	Name,
	// Token: 0x0400116B RID: 4459
	CreateAccountQuestion,
	// Token: 0x0400116C RID: 4460
	DoYouWantCreate,
	// Token: 0x0400116D RID: 4461
	PermissionRequired,
	// Token: 0x0400116E RID: 4462
	NeedPermissionText,
	// Token: 0x0400116F RID: 4463
	GuardianEmailTitle,
	// Token: 0x04001170 RID: 4464
	Send,
	// Token: 0x04001171 RID: 4465
	NewEmail,
	// Token: 0x04001172 RID: 4466
	ConfirmEmail,
	// Token: 0x04001173 RID: 4467
	EditEmail,
	// Token: 0x04001174 RID: 4468
	Loading,
	// Token: 0x04001175 RID: 4469
	Welcome
}
