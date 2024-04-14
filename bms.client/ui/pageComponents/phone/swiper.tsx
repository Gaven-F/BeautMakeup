"use client";

import { times } from "lodash";
import { Autoplay, EffectCards, Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css/pagination";
import "swiper/css/effect-cards";

export default function PhoneSwiper() {
	return (
		<Swiper
			autoplay={{ delay: 1500 }}
			pagination
			effect="cards"
			className="w-[80vw] h-[33vh] bg-opacity-25 rounded my-2"
			modules={[Autoplay, Pagination, EffectCards]}
		>
			{times(12, (i) => (
				<SwiperSlide
					key={i}
					className={
						"h-[30vh] w-20 !flex place-content-center place-items-center rounded opacity-90 backdrop-blur-3xl " +
						"[&:nth-child(1n)]:bg-red-50 [&:nth-child(2n)]:bg-green-100 [&:nth-child(3n)]:bg-yellow-200 " +
						"[&:nth-child(4n)]:bg-blue-300 [&:nth-child(5n)]:bg-sky-400 [&:nth-child(6n)]:bg-violet-500 "
					}
				>
					<div className="">图片{i + 1}</div>
				</SwiperSlide>
			))}
		</Swiper>
	);
}
