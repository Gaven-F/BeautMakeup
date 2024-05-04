"use client";

import { usePathname } from "next/navigation";

// Code: PhoneLayout
type PhoneLayoutProps = {
	children: React.ReactNode;
	header: React.ReactNode;
	footer: React.ReactNode;
};

export default function PhoneLayout({
	children,
	header,
	footer,
}: PhoneLayoutProps) {
	const path = usePathname();
	return (
		<div className="flex flex-col w-full h-full">
			<div className="h-0 w-full flex-1 grid-container overflow-y-auto bg-sky-50">
				{path.endsWith("phone") && header}
				{children}
			</div>
			<div className="h-16">{footer}</div>
		</div>
	);
}
