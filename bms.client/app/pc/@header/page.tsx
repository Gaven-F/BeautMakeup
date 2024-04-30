"use client";

import { useRouter, usePathname } from "next/navigation";

export default function PcHeader() {
	const router = useRouter();
	const pathName = usePathname();
	console.log(router, pathName);
	return <>Header</>;
}
