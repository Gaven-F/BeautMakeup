import PhoneSwiper from "./@ui/swiper";
import { times } from "lodash";
import Merchandise from "./@ui/merchandise";

export default function PhonePage() {
	return (
		<>
			<PhoneSwiper></PhoneSwiper>
			<div className="h-[calc(100vh-9rem)] py-2">
				<div className="bg-blue-300 bg-opacity-25 h-full rounded overflow-auto grid grid-cols-2 auto-rows-min gap-4 p-2">
					{times(37, (i) => (
						<Merchandise></Merchandise>
					))}
				</div>
			</div>
		</>
	);
}
