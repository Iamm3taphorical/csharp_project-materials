'use client';
import { LiquidGlassCard } from "@/components/ui/liquid-weather-glass";
import {
    Cloud,
    CloudSun,
    CloudRain,
    Sun,
    MapPin,
    CloudSunRain,
    Snowflake,
    CloudFog,
    Moon,
} from 'lucide-react';
import React, { useState, useEffect } from 'react';
import { cn } from "@/lib/utils";

type WeatherType = 'sunny' | 'cloudy' | 'rainy' | 'snowy' | 'clear';
type TimeType = 'day' | 'night';

export default function DemoOne() {
    const [time, setTime] = useState<TimeType>('day');
    const [weather, setWeather] = useState<WeatherType>('sunny');

     
    const backgrounds: Record<string, string> = {
        'day-sunny': 'https://images.unsplash.com/photo-1622396481328-9b1b78cdd9fd?q=80&w=1974&auto=format&fit=crop',  
        'day-cloudy': 'https://images.unsplash.com/photo-1595865728048-9a9107e0f878?q=80&w=2072&auto=format&fit=crop',  
        'day-rainy': 'https://images.unsplash.com/photo-1519692933481-e162a57d6721?q=80&w=2070&auto=format&fit=crop',  
        'day-snowy': 'https://images.unsplash.com/photo-1483664852095-d6cc6870705d?q=80&w=2070&auto=format&fit=crop',  

        'night-clear': 'https://images.unsplash.com/photo-1532074551798-a621d1d86f38?q=80&w=1974&auto=format&fit=crop',  
        'night-cloudy': 'https://images.unsplash.com/photo-1536244636800-a3f74db0f3cf?q=80&w=1984&auto=format&fit=crop',  
        'night-rainy': 'https://images.unsplash.com/photo-1515694346937-94d85e41e6f0?q=80&w=1974&auto=format&fit=crop',  
        'night-snowy': 'https://images.unsplash.com/photo-1517482812165-385ee9193155?q=80&w=2069&auto=format&fit=crop',  
    };

    const currentBg = backgrounds[`${time}-${weather}`] || backgrounds['day-sunny'];

    return (
        <div className="flex flex-col gap-4 w-full max-w-4xl mx-auto">
            { }
            <div className="flex gap-4 p-4 bg-background/50 backdrop-blur-md rounded-lg mx-auto w-full max-w-xl justify-center z-50">
                <select
                    value={time}
                    onChange={(e) => setTime(e.target.value as TimeType)}
                    className="p-2 rounded border bg-card text-card-foreground"
                >
                    <option value="day">Day</option>
                    <option value="night">Night</option>
                </select>
                <select
                    value={weather}
                    onChange={(e) => setWeather(e.target.value as WeatherType)}
                    className="p-2 rounded border bg-card text-card-foreground"
                >
                    <option value="sunny">Sunny / Clear</option>
                    <option value="cloudy">Cloudy</option>
                    <option value="rainy">Rainy</option>
                    <option value="snowy">Snowy</option>
                </select>
            </div>

            <div
                className='p-8 w-full gap-8 py-16 rounded-xl relative overflow-hidden min-h-[600px] transition-all duration-1000'
                style={{
                    background: `url("${currentBg}") center / cover no-repeat`,
                }}
            >
                { }
                <div className={cn("absolute inset-0 transition-opacity duration-1000",
                    time === 'night' ? 'bg-black/40' : 'bg-black/10'
                )} />

                <div className='relative z-10 grid w-full max-w-xl grid-cols-2 gap-4 mx-auto'>
                    { }
                    <LiquidGlassCard
                        shadowIntensity='xs'
                        borderRadius='16px'
                        glowIntensity='none'
                        className='col-span-2 p-6 text-white bg-white/10'
                    >
                        <div className='flex justify-between text-sm font-medium'>
                            <div className='flex flex-col items-center gap-2'>
                                <span>16:00</span>
                                <Cloud className='h-6 w-6 fill-white/80' />
                                <span>+18°</span>
                            </div>
                            <div className='flex flex-col items-center gap-2'>
                                <span>17:00</span>
                                <Cloud className='h-6 w-6 fill-white/80' />
                                <span>+18°</span>
                            </div>
                            <div className='flex flex-col items-center gap-2'>
                                <span>18:00</span>
                                <CloudRain className='h-6 w-6' />
                                <span>+16°</span>
                            </div>
                        </div>
                    </LiquidGlassCard>

                    { }
                    <LiquidGlassCard
                        shadowIntensity='xs'
                        borderRadius='32px'
                        glowIntensity='sm'
                        className={cn('rounded-[32px] p-6 text-white bg-white/10 flex flex-col items-start justify-center')}
                    >
                        <div className='text-6xl font-semibold'>
                            {weather === 'snowy' ? '-2°C' : '+18°C'}
                        </div>
                        <div className='text-lg capitalize flex items-center gap-2'>
                            {weather === 'sunny' && <Sun className="h-5 w-5 fill-yellow-400 text-yellow-400" />}
                            {weather === 'cloudy' && <Cloud className="h-5 w-5" />}
                            {weather === 'rainy' && <CloudRain className="h-5 w-5" />}
                            {weather === 'snowy' && <Snowflake className="h-5 w-5" />}
                            {weather}
                        </div>
                    </LiquidGlassCard>

                    { }
                    <LiquidGlassCard
                        shadowIntensity='xs'
                        borderRadius='32px'
                        glowIntensity='sm'
                        className='rounded-[32px] p-6 text-white bg-white/10 flex flex-col items-start justify-center'
                    >
                        <div className='text-6xl font-semibold'>17:32</div>
                        <div className='text-lg flex items-center gap-2'>
                            {time === 'day' ? <Sun className="h-4 w-4" /> : <Moon className="h-4 w-4" />}
                            {time === 'day' ? 'Afternoon' : 'Evening'}
                        </div>
                        <button className='mt-4 inline-flex items-center gap-1 rounded-full bg-black/20 hover:bg-black/30 backdrop-blur-md px-3 py-1 text-sm font-medium transition-colors'>
                            <MapPin className='h-4 w-4' />
                            Tbilisi
                        </button>
                    </LiquidGlassCard>

                    { }
                    <LiquidGlassCard
                        shadowIntensity='xs'
                        borderRadius='32px'
                        glowIntensity='none'
                        className='col-span-2 rounded-[32px] bg-white/10 p-6 text-white flex flex-col gap-4'
                    >
                        <div className='flex items-center justify-between'>
                            <div className='flex items-center gap-2'>
                                <Sun className='h-6 w-6 fill-white/90' />
                                <span>Tue, 7 Sep</span>
                            </div>
                            <span className='text-lg'>+18°/+4°</span>
                        </div>
                        <div className='flex items-center justify-between'>
                            <div className='flex items-center gap-2'>
                                <CloudRain className='h-6 w-6' />
                                <span>Wed, 8 Sep</span>
                            </div>
                            <span className='text-lg'>+17°/+3°</span>
                        </div>
                    </LiquidGlassCard>
                </div>
            </div>
        </div>
    );
}
