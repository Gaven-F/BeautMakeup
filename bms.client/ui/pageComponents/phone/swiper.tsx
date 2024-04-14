"use client";

import { times } from "lodash";
import { Autoplay, Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import "swiper/css/pagination";

export default function PhoneSwiper() {
	return (
		<Swiper
			loop
			autoplay={{ delay: 3000 }}
			pagination
			className="w-full h-[33vh] bg-slate-500 bg-opacity-25 rounded my-2"
			modules={[Autoplay, Pagination]}
		>
			{times(3, (i) => (
				<SwiperSlide
					key={i}
					className="h-[30vh] !flex place-content-center place-items-center"
				>
					<div>图片{i + 1}</div>
				</SwiperSlide>
			))}
		</Swiper>
	);
}
