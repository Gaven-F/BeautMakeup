import PhoneSwiper from "@/components/swiper";
import { times } from "lodash";
import Merchandise from "@/components/merchandise";

export default function PhonePage() {
	return (
		<>
			<PhoneSwiper></PhoneSwiper>
			<div className="flex-1 py-2">
				<div className="bg-blue-300 bg-opacity-25 h-full rounded overflow-auto grid grid-cols-2 auto-rows-min gap-4 p-2">
					{times(37, (i) => (
						<Merchandise key={i}></Merchandise>
					))}
				</div>
			</div>
		</>
	);
}
