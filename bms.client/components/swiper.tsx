"use client";

import { times } from "lodash";
import { Autoplay, EffectCards, Navigation, Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css/pagination";
import "swiper/css/navigation";

export default function PhoneSwiper() {
	return (
		<Swiper
			autoplay={{ delay: 1500 }}
			pagination
			loop
			navigation
			className="!w-[90vw] h-[28vh] bg-opacity-25 rounded my-2"
			modules={[Autoplay, Pagination, Navigation]}>
			{times(12, (i) => (
				<SwiperSlide
					key={i}
					className="flex bg-slate-300">
					<div className="m-auto">图片{i + 1}</div>
				</SwiperSlide>
			))}
		</Swiper>
	);
}
