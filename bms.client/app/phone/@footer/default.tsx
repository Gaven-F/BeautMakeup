"use client";

import { Button } from "antd";
import { usePathname, useRouter } from "next/navigation";
import { Suspense } from "react";

const funMenu = [
	{ label: "首页", icon: "home", path: "/phone" },
	{ label: "商店", icon: "home", path: "/phone/shop" },
	{ label: "个人中心", icon: "home", path: "/phone/personalCenter" },
];

export default function footer() {
	const pathName = usePathname();
	const router = useRouter();
	return (
		<div className="flex h-full bg-blue-50">
			{funMenu.map((it) => (
				<Button
					shape="round"
					className={"m-auto"}
					onClick={() => {
						router.push(it.path, { scroll: false });
					}}
					type={pathName.endsWith(it.path) ? "primary" : "default"}
					key={it.path}>
					{it.label}
				</Button>
			))}
		</div>
	);
}
