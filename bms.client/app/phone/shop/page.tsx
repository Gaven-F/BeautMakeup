"use client";

import Merchandise from "@/components/merchandise";
import { Input, Select } from "antd";
import { times } from "lodash";

const searchOptions = {
	type: ["全部", "口红", "眼影"].map((it) => ({ value: it })),
	price: ["全部", "≤500", "≥500"].map((it) => ({ value: it })),
};

export default function PhoneShop() {
	return (
		<>
			<div className="top-0 sticky z-10 bg-sky-50 full px-2">
				<div className="flex py-2 justify-around">
					<div>
						分类：
						<Select
							options={searchOptions.type}
							defaultValue={searchOptions.type[0]}
						/>
					</div>
					<div>
						价格：
						<Select
							options={searchOptions.price}
							defaultValue={searchOptions.price[0]}
						/>
					</div>
				</div>
				<div className="flex py-2">
					<Input.Search placeholder="搜索商品" />
				</div>
			</div>
			<div className="full grid grid-cols-2 gap-4 auto-rows-min h-full overflow-auto">
				{times(37, (i) => (
					<Merchandise key={i}></Merchandise>
				))}
			</div>
		</>
	);
}
